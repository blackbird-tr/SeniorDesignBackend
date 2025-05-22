# SignalR React Entegrasyonu

Bu dokümantasyon, backend'deki SignalR hub'ına React uygulamasından nasıl bağlanılacağını açıklar.

## 1. Gereksinimler

React projenize SignalR client'ı ekleyin:

```bash
npm install @microsoft/signalr
# veya
yarn add @microsoft/signalr
```

## 2. SignalR Bağlantı Servisi

`src/services/signalRService.js` dosyası oluşturun:

```javascript
import * as signalR from "@microsoft/signalr";

class SignalRService {
    constructor() {
        this.connection = null;
        this.handlers = new Map();
    }

    startConnection = async (token) => {
        try {
            // Hub URL'inizi buraya yazın
            const hubUrl = "https://localhost:7001/notificationHub";

            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(hubUrl, {
                    accessTokenFactory: () => token
                })
                .withAutomaticReconnect()
                .build();

            // Bildirim geldiğinde çalışacak fonksiyon
            this.connection.on("ReceiveNotification", (notification) => {
                // Bildirimi işle
                this.handleNotification(notification);
            });

            // Bağlantıyı başlat
            await this.connection.start();
            console.log("SignalR Connected!");
        } catch (err) {
            console.error("SignalR Connection Error: ", err);
        }
    };

    handleNotification = (notification) => {
        // Bildirimi işle ve UI'ı güncelle
        if (this.handlers.has("notification")) {
            this.handlers.get("notification")(notification);
        }
    };

    // Bildirim handler'ı ekle
    addNotificationHandler = (handler) => {
        this.handlers.set("notification", handler);
    };

    // Bağlantıyı durdur
    stopConnection = async () => {
        try {
            await this.connection.stop();
            console.log("SignalR Disconnected!");
        } catch (err) {
            console.error("SignalR Disconnection Error: ", err);
        }
    };
}

export default new SignalRService();
```

## 3. React Component'te Kullanım

```jsx
import React, { useEffect, useState } from 'react';
import signalRService from '../services/signalRService';

function NotificationComponent() {
    const [notifications, setNotifications] = useState([]);

    useEffect(() => {
        // Login'den alınan token
        const token = localStorage.getItem('token');

        // SignalR bağlantısını başlat
        signalRService.startConnection(token);

        // Bildirim handler'ını ekle
        signalRService.addNotificationHandler((notification) => {
            setNotifications(prev => [...prev, notification]);
        });

        // Component unmount olduğunda bağlantıyı kapat
        return () => {
            signalRService.stopConnection();
        };
    }, []);

    return (
        <div>
            <h2>Bildirimler</h2>
            <div className="notifications">
                {notifications.map((notification, index) => (
                    <div key={index} className="notification">
                        <h3>{notification.title}</h3>
                        <p>{notification.message}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default NotificationComponent;
```

## 4. Bildirim Stili

```css
.notifications {
    max-height: 400px;
    overflow-y: auto;
}

.notification {
    padding: 10px;
    margin: 5px 0;
    border-radius: 4px;
    background-color: #f5f5f5;
    border-left: 4px solid #007bff;
}

.notification h3 {
    margin: 0;
    font-size: 16px;
    color: #333;
}

.notification p {
    margin: 5px 0 0;
    font-size: 14px;
    color: #666;
}
```

## 5. Bildirim API Servisi

```javascript
// src/services/notificationApi.js
import axios from 'axios';

const API_URL = 'https://localhost:7001/api';

export const notificationApi = {
    getNotifications: async () => {
        const response = await axios.get(`${API_URL}/notification`);
        return response.data;
    },

    markAsRead: async (id) => {
        await axios.put(`${API_URL}/notification/${id}/mark-as-read`);
    },

    markAllAsRead: async () => {
        await axios.put(`${API_URL}/notification/mark-all-as-read`);
    },

    getUnreadCount: async () => {
        const response = await axios.get(`${API_URL}/notification/unread-count`);
        return response.data.count;
    }
};
```

## 6. Bildirim Yönetimi Component'i

```jsx
import React, { useEffect, useState } from 'react';
import { notificationApi } from '../services/notificationApi';
import signalRService from '../services/signalRService';

function NotificationManager() {
    const [notifications, setNotifications] = useState([]);
    const [unreadCount, setUnreadCount] = useState(0);

    useEffect(() => {
        // İlk yüklemede bildirimleri getir
        loadNotifications();
        
        // SignalR bağlantısını başlat
        const token = localStorage.getItem('token');
        signalRService.startConnection(token);

        // Yeni bildirim geldiğinde
        signalRService.addNotificationHandler((notification) => {
            setNotifications(prev => [notification, ...prev]);
            setUnreadCount(prev => prev + 1);
        });

        return () => {
            signalRService.stopConnection();
        };
    }, []);

    const loadNotifications = async () => {
        try {
            const [notifications, unreadCount] = await Promise.all([
                notificationApi.getNotifications(),
                notificationApi.getUnreadCount()
            ]);
            setNotifications(notifications);
            setUnreadCount(unreadCount);
        } catch (error) {
            console.error('Error loading notifications:', error);
        }
    };

    const handleMarkAsRead = async (id) => {
        try {
            await notificationApi.markAsRead(id);
            setNotifications(prev =>
                prev.map(n => n.id === id ? { ...n, isRead: true } : n)
            );
            setUnreadCount(prev => Math.max(0, prev - 1));
        } catch (error) {
            console.error('Error marking notification as read:', error);
        }
    };

    const handleMarkAllAsRead = async () => {
        try {
            await notificationApi.markAllAsRead();
            setNotifications(prev =>
                prev.map(n => ({ ...n, isRead: true }))
            );
            setUnreadCount(0);
        } catch (error) {
            console.error('Error marking all notifications as read:', error);
        }
    };

    return (
        <div className="notification-manager">
            <div className="notification-header">
                <h2>Bildirimler ({unreadCount} okunmamış)</h2>
                {unreadCount > 0 && (
                    <button onClick={handleMarkAllAsRead}>
                        Tümünü Okundu İşaretle
                    </button>
                )}
            </div>
            <div className="notifications">
                {notifications.map(notification => (
                    <div
                        key={notification.id}
                        className={`notification ${notification.isRead ? 'read' : 'unread'}`}
                    >
                        <h3>{notification.title}</h3>
                        <p>{notification.message}</p>
                        {!notification.isRead && (
                            <button onClick={() => handleMarkAsRead(notification.id)}>
                                Okundu İşaretle
                            </button>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}

export default NotificationManager;
```

## 7. Önemli Noktalar

1. SignalR bağlantısı için JWT token'ı kullanılıyor
2. Bağlantı otomatik yeniden bağlanma özelliğine sahip
3. Bildirimler real-time olarak alınıyor
4. Okunmamış bildirim sayısı takip ediliyor
5. Bildirimleri okundu olarak işaretleme özelliği var

## 8. Test Etme

1. Uygulamayı başlatın
2. Console'da bağlantı durumunu kontrol edin
3. Backend'den test bildirimi gönderin
4. Bildirimlerin UI'da görüntülendiğini doğrulayın 