using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Extensions
{
    public static class htmlMail
    {

        public static string ToResetPasswordHtmlBody(this string token, string userName)
        {
            return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='UTF-8'>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f0f2f5;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: auto;
                    background-color: #ffffff;
                    border-radius: 8px;
                    padding: 30px;
                    box-shadow: 0 0 10px rgba(0,0,0,0.05);
                }}
                .code {{
                    font-size: 24px;
                    font-weight: bold;
                    background-color: #e3f2fd;
                    color: #0d6efd;
                    padding: 15px;
                    text-align: center;
                    border-radius: 6px;
                    margin: 24px 0;
                    word-break: break-word;
                }}
                .footer {{
                    font-size: 12px;
                    color: #888;
                    text-align: center;
                    margin-top: 30px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Şifre Sıfırlama</h2>
                <p>Merhaba <strong>{userName}</strong>,</p>
                <p>Aşağıdaki token ile şifreni sıfırlayabilirsin:</p>
                <div class='code'>{token}</div>
                <p>Bu token sadece kısa süre geçerlidir. Eğer bu işlemi sen başlatmadıysan, bu e-postayı görmezden gelebilirsin.</p>
                <div class='footer'>&copy; {DateTime.Now.Year} Logistify</div>
            </div>
        </body>
        </html>";
        }

        public static string ToEmailConfirmationHtmlBody(this string code)
        {
            return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='UTF-8'>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: auto;
                    background-color: #fff;
                    border-radius: 8px;
                    padding: 30px;
                    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
                }}
                h2 {{
                    color: #333;
                }}
                .code {{
                    font-size: 28px;
                    font-weight: bold;
                    background-color: #e3f2fd;
                    color: #1a73e8;
                    padding: 12px;
                    border-radius: 6px;
                    text-align: center;
                    margin: 24px 0;
                }}
                .footer {{
                    font-size: 12px;
                    color: #777;
                    text-align: center;
                    margin-top: 30px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Email Confirmation</h2>
                <p>Hello,</p>
                <p>Use the confirmation code below to verify your email address:</p>
                <div class='code'>{code}</div>
                <p>If you didn't request this, please ignore this email.</p>
                <div class='footer'>&copy; {DateTime.Now.Year} Logistify</div>
            </div>
        </body>
        </html>";
        }

        public static string ToVehicleAdMailBody(this VehicleAd ad)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }}
        .container {{ max-width: 600px; margin: auto; background-color: #fff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }}
        h2 {{ color: #198754; }}
        .info {{ font-size: 15px; margin: 15px 0; }}
        .footer {{ font-size: 12px; color: #777; text-align: center; margin-top: 30px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Araç İlanınız Onaylandı</h2>
        <p class='info'><strong>Başlık:</strong> {ad.Title}</p>
        <p class='info'><strong>Taşıma Kapasitesi:</strong> {ad.Capacity} ton</p>
        <p class='info'><strong>Araç Tipi:</strong> {ad.VehicleType}</p>
        <p class='info'><strong>Konum:</strong> {ad.City}, {ad.Country}</p>
        <p class='info'><strong>İlan Tarihi:</strong> {ad.AdDate:dd.MM.yyyy}</p>
        <div class='footer'>&copy; {DateTime.Now.Year} Logistify</div>
    </div>
</body>
</html>";
        }

        public static string ToVehicleOfferMailBody(this VehicleOffer offer)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px; }}
        .container {{ max-width: 600px; margin: auto; background-color: #fff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }}
        h2 {{ color: #0d6efd; }}
        .info {{ font-size: 15px; margin: 15px 0; }}
        .footer {{ font-size: 12px; color: #777; text-align: center; margin-top: 30px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Araç Teklifiniz Güncellendi</h2>
        <p class='info'><strong>İlan Başlığı:</strong> {offer.VehicleAd.Title}</p>
        <p class='info'><strong>Teklif Mesajı:</strong> {offer.Message}</p>
        <p class='info'><strong>Geçerlilik Tarihi:</strong> {offer.ExpiryDate?.ToString("dd.MM.yyyy") ?? "Belirtilmedi"}</p>
        <div class='footer'>&copy; {DateTime.Now.Year} Logistify</div>
    </div>
</body>
</html>";
        }

        public static string ToCargoAdMailBody(this CargoAd ad)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f0f2f5; padding: 20px; }}
        .container {{ max-width: 600px; margin: auto; background-color: #fff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }}
        h2 {{ color: #198754; }}
        .info {{ font-size: 15px; margin: 15px 0; }}
        .footer {{ font-size: 12px; color: #777; text-align: center; margin-top: 30px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Kargo İlanınız Onaylandı</h2>
        <p class='info'><strong>Başlık:</strong> {ad.Title}</p>
        <p class='info'><strong>Kargo Tipi:</strong> {ad.CargoType}</p>
        <p class='info'><strong>Ağırlık:</strong> {ad.Weight} kg</p>
        <p class='info'><strong>Nereden:</strong> {ad.PickCity}, {ad.PickCountry}</p>
        <p class='info'><strong>Nereye:</strong> {ad.DropCity}, {ad.DropCountry}</p>
        <p class='info'><strong>Fiyat:</strong> {ad.Price} {ad.currency}</p>
        <div class='footer'>&copy; {DateTime.Now.Year} Logistify</div>
    </div>
</body>
</html>";
        }

        public static string ToCargoOfferMailBody(this CargoOffer offer)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px; }}
        .container {{ max-width: 600px; margin: auto; background-color: #fff; border-radius: 8px; padding: 30px; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }}
        h2 {{ color: #0d6efd; }}
        .info {{ font-size: 15px; margin: 15px 0; }}
        .footer {{ font-size: 12px; color: #777; text-align: center; margin-top: 30px; }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Kargo Teklifiniz Güncellendi</h2>
        <p class='info'><strong>İlan Başlığı:</strong> {offer.CargoAd.Title}</p>
        <p class='info'><strong>Teklif Mesajı:</strong> {offer.Message}</p>
        <p class='info'><strong>Fiyat:</strong> {offer.Price} {offer.CargoAd.currency}</p>
        <p class='info'><strong>Geçerlilik Tarihi:</strong> {offer.ExpiryDate?.ToString("dd.MM.yyyy") ?? "Belirtilmedi"}</p>
        <div class='footer'>&copy; {DateTime.Now.Year} Logistify</div>
    </div>
</body>
</html>";
        }



    }
}
