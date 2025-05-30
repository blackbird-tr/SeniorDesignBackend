# Account Service

📌 **Project Overview**

Account Service is a microservice designed for robust authentication and user management. It provides essential functionalities such as user registration, login, email confirmation, JWT and refresh token management, password changes/resets, and user information updates.

**Key Features:**

* **User Registration (Register):** Seamless user onboarding.
* **User Login (Login):** Secure authentication process.
* **Email Confirmation:** Verification of user email addresses.
* **JWT and Refresh Token Management:** Secure and efficient token-based authentication.
* **Change/Reset Password:** User-friendly password management.
* **Update User Information:** Ability to modify user details.

---

🏗️ **Architecture**

This service adopts a **Vertical Slice Architecture** combined with **CQRS (Command Query Responsibility Segregation)** to enhance maintainability and scalability.


📂 AccountService  
 ┣ 📂 Application (Business logic & CQRS commands)  
 ┃ ┣ 📂 Features (Each operation as a separate slice)  
 ┃ ┃ ┣ 📂 Users (User-related operations)  
 ┃ ┃ ┃ ┣ 📂 Commands (Register, Login, Update, Password Reset, etc.)  
 ┃ ┃ ┃ ┣ 📂 Queries (Fetching user details, etc.)  
 ┃ ┃ ┃ ┗ 📂 Validators (FluentValidation for input validation)  
 ┃ ┣ 📂 Common (Global utilities - JWT, hashing, etc.)  
 ┣ 📂 Infrastructure (Database & external dependencies)  
 ┃ ┣ 📂 Persistence (MSSQL, Entity Framework operations)  
 ┃ ┣ 📂 Authentication (JWT, Refresh Token, Identity management)  
 ┣ 📂 API (Controllers)  
 ┣ 📂 Domain (Core entities - e.g., User Entity)  

---

🚀 **Technologies Used**

* **ASP.NET Core Web API:** Building the RESTful API.
* **Entity Framework Core (MSSQL):** Object-relational mapping for database interactions.
* **MediatR (for CQRS pattern):** Implementing the CQRS pattern for clean separation of concerns.
* **FluentValidation (for input validation):** Ensuring data integrity through robust validation.
* **JWT + Refresh Token (Authentication):** Secure token-based authentication and authorization.

---

⚙️ **Installation & Setup**

**Steps to get the service running:**

1.  **Install Dependencies:**

    ```bash
    dotnet restore
    ```

2.  **Apply Database Migrations:**

    ```bash
    dotnet ef database update
    ```

3.  **Run the Service:**

    ```bash
    dotnet run
    ```

---

🔗 **API Endpoints**

**🟢 Authentication:**

| HTTP Method | Endpoint                 | Description                                    |
| :---------- | :----------------------- | :--------------------------------------------- |
| POST        | `/api/auth/register`      | Registers a new user.                               |
| POST        | `/api/auth/login`         | Authenticates a user and returns JWT.               |
| POST        | `/api/auth/refresh-token` | Generates a new JWT using a refresh token.          |
| POST        | `/api/auth/confirm-email` | Confirms user email address.                          |
| POST        | `/api/auth/change-password`| Changes user password.                                |

---



📜 **License**

This project is licensed under the **MIT License**.

# SignalR Android Entegrasyonu

Bu dokümantasyon, backend'deki SignalR hub'ına Android uygulamasından nasıl bağlanılacağını açıklar.

## 1. Gereksinimler

Android projenizin `build.gradle` dosyasına aşağıdaki dependency'i ekleyin:

```gradle
dependencies {
    implementation 'com.microsoft.signalr:signalr:7.0.0'
}
```

## 2. SignalR Servisi

SignalR bağlantısını yönetmek için bir servis sınıfı oluşturun:

```kotlin
class SignalRService {
    private var hubConnection: HubConnection? = null
    
    fun startConnection(username: String) {
        // Hub URL'inizi buraya yazın
        val hubUrl = "https://your-api-url/notificationHub"
        
        hubConnection = HubConnectionBuilder.create(hubUrl)
            .withHeader("Authorization", "Bearer $token") // JWT token'ınızı buraya ekleyin
            .build()
            
        // Bildirim geldiğinde çalışacak fonksiyon
        hubConnection?.on("ReceiveNotification") { notification ->
            // Gelen bildirimi işleyin
            val notificationJson = notification.toString()
            // JSON'ı parse edip bildirimi gösterin
        }
        
        // Bağlantıyı başlat
        hubConnection?.start()?.blockingAwait()
    }
    
    fun stopConnection() {
        hubConnection?.stop()
    }
}
```

## 3. Bildirim Yöneticisi

Bildirimleri göstermek için bir NotificationManager sınıfı:

```kotlin
class NotificationManager(private val context: Context) {
    private val notificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
    
    fun showNotification(notification: Notification) {
        // Bildirim kanalı oluştur
        val channelId = "notification_channel"
        val channelName = "Notifications"
        val importance = NotificationManager.IMPORTANCE_HIGH
        
        val channel = NotificationChannel(channelId, channelName, importance)
        notificationManager.createNotificationChannel(channel)
        
        // Bildirimi oluştur
        val builder = NotificationCompat.Builder(context, channelId)
            .setSmallIcon(R.drawable.ic_notification)
            .setContentTitle(notification.title)
            .setContentText(notification.message)
            .setPriority(NotificationCompat.PRIORITY_HIGH)
            .setAutoCancel(true)
        
        // Bildirimi göster
        notificationManager.notify(notification.id, builder.build())
    }
}
```

## 4. Ana Aktivitede Kullanım

```kotlin
class MainActivity : AppCompatActivity() {
    private lateinit var signalRService: SignalRService
    private lateinit var notificationManager: NotificationManager
    
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        
        notificationManager = NotificationManager(this)
        signalRService = SignalRService()
        
        // Kullanıcı giriş yaptıktan sonra SignalR bağlantısını başlat
        val username = getUsernameFromSharedPrefs() // Kullanıcı adını SharedPreferences'dan al
        signalRService.startConnection(username)
    }
    
    override fun onDestroy() {
        super.onDestroy()
        signalRService.stopConnection()
    }
}
```

## 5. Bildirim Modeli

```kotlin
data class Notification(
    val id: Int,
    val title: String,
    val message: String,
    val type: String,
    val isRead: Boolean,
    val createdDate: String,
    val relatedEntityId: Int?
)
```

## 6. API Servisi

Bildirimleri çekmek ve yönetmek için API servisi:

```kotlin
interface NotificationApi {
    @GET("api/notification")
    suspend fun getNotifications(): List<Notification>
    
    @PUT("api/notification/{id}/mark-as-read")
    suspend fun markAsRead(@Path("id") id: Int)
    
    @PUT("api/notification/mark-all-as-read")
    suspend fun markAllAsRead()
    
    @GET("api/notification/unread-count")
    suspend fun getUnreadCount(): Int
}
```

## 7. Arka Plan Servisi

Uygulama arka plandayken bile bildirimleri alabilmek için bir Service sınıfı:

```kotlin
class NotificationService : Service() {
    private var signalRService: SignalRService? = null
    
    override fun onStartCommand(intent: Intent?, flags: Int, startId: Int): Int {
        val username = intent?.getStringExtra("username") ?: return START_NOT_STICKY
        
        signalRService = SignalRService()
        signalRService?.startConnection(username)
        
        return START_STICKY
    }
    
    override fun onBind(intent: Intent?): IBinder? = null
    
    override fun onDestroy() {
        super.onDestroy()
        signalRService?.stopConnection()
    }
}
```

## 8. Önemli Noktalar

1. SignalR bağlantısı için JWT token'ı header'a eklemeyi unutmayın
2. Bildirimleri göstermek için Android'in NotificationManager'ını kullanın
3. Uygulama arka plandayken bile bildirimleri alabilmek için bir Service sınıfı kullanın
4. Bağlantı koptuğunda otomatik yeniden bağlanma için retry mekanizması ekleyin

## 9. Bağlantı Kopması Durumu

Bağlantı koptuğunda otomatik yeniden bağlanma için:

```kotlin
hubConnection?.onClosed { exception ->
    // Bağlantı koptuğunda yeniden bağlan
    if (exception != null) {
        // Hata durumunda 5 saniye bekle ve yeniden dene
        Handler(Looper.getMainLooper()).postDelayed({
            startConnection(username)
        }, 5000)
    }
}
```

## 10. Hata Yönetimi

```kotlin
try {
    hubConnection?.start()?.blockingAwait()
} catch (e: Exception) {
    // Bağlantı hatası durumunda kullanıcıya bilgi ver
    Toast.makeText(context, "Bildirim bağlantısı kurulamadı", Toast.LENGTH_SHORT).show()
}
```

## 11. İzinler

AndroidManifest.xml dosyasına aşağıdaki izinleri ekleyin:

```xml
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.POST_NOTIFICATIONS" />
```

## 12. Test Etme

SignalR bağlantısını test etmek için:

1. Uygulamayı başlatın
2. Logcat'te bağlantı durumunu kontrol edin
3. Backend'den test bildirimi gönderin
4. Bildirimin Android cihazda görüntülendiğini doğrulayın
