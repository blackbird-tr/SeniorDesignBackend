using AccountService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Extensions
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


        public static string ToVehicleOfferMailBody(this VehicleOffer offer)
        {
            return $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            margin: 0;
            padding: 30px;
            background-color: #f0f2f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #212529;
        }}
        .card {{
            max-width: 640px;
            margin: auto;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
        }}
        .card-header {{
            background-color: #0d6efd;
            color: #ffffff;
            padding: 20px 30px;
            font-size: 20px;
            font-weight: bold;
        }}
        .card-body {{
            padding: 30px;
        }}
        .info {{
            margin: 15px 0;
            font-size: 15px;
            line-height: 1.6;
        }}
        .info strong {{
            display: inline-block;
            width: 140px;
            color: #495057;
        }}
        .card-footer {{
            padding: 20px;
            text-align: center;
            font-size: 13px;
            color: #6c757d;
            background-color: #f8f9fa;
            border-top: 1px solid #e9ecef;
        }}
        @media (max-width: 600px) {{
            .card-body, .card-header, .card-footer {{
                padding: 20px;
            }}
            .info strong {{
                display: block;
                margin-bottom: 4px;
            }}
        }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='card-header'>Araç Teklifiniz Güncellendi</div>
        <div class='card-body'>
            <div class='info'><strong>İlan Başlığı:</strong> {offer.VehicleAd.Title}</div>
            <div class='info'><strong>Teklif Mesajı:</strong> {offer.Message}</div>
            <div class='info'><strong>Geçerlilik Tarihi:</strong> {offer.ExpiryDate?.ToString("dd.MM.yyyy") ?? "Belirtilmedi"}</div>
        </div>
        <div class='card-footer'>&copy; {DateTime.Now.Year} Logistify – Tüm hakları saklıdır.</div>
    </div>
</body>
</html>";
        }
        public static string ToVehicleAdMailBody(this VehicleAd ad)
        {
            return $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            margin: 0;
            padding: 30px;
            background-color: #f0f2f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #212529;
        }}
        .card {{
            max-width: 640px;
            margin: auto;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
        }}
        .card-header {{
            background-color: #0d6efd;
            color: #ffffff;
            padding: 20px 30px;
            font-size: 20px;
            font-weight: bold;
        }}
        .card-body {{
            padding: 30px;
        }}
        .info {{
            margin: 15px 0;
            font-size: 15px;
            line-height: 1.6;
        }}
        .info strong {{
            display: inline-block;
            width: 140px;
            color: #495057;
        }}
        .card-footer {{
            padding: 20px;
            text-align: center;
            font-size: 13px;
            color: #6c757d;
            background-color: #f8f9fa;
            border-top: 1px solid #e9ecef;
        }}
        @media (max-width: 600px) {{
            .card-body, .card-header, .card-footer {{
                padding: 20px;
            }}
            .info strong {{
                display: block;
                margin-bottom: 4px;
            }}
        }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='card-header'>Araç İlanınız Güncellendi</div>
        <div class='card-body'>
            <div class='info'><strong>Başlık:</strong> {ad.Title}</div>
            <div class='info'><strong>Açıklama:</strong> {ad.Desc}</div>
            <div class='info'><strong>Araç Tipi:</strong> {ad.VehicleType}</div>
            <div class='info'><strong>Kapasite:</strong> {ad.Capacity} ton</div>
            <div class='info'><strong>Konum:</strong> {ad.City}, {ad.Country}</div>
            <div class='info'><strong>İlan Tarihi:</strong> {ad.AdDate:dd.MM.yyyy}</div>
        </div>
        <div class='card-footer'>&copy; {DateTime.Now.Year} Logistify – Tüm hakları saklıdır.</div>
    </div>
</body>
</html>";
        }

        public static string ToCargoAdMailBody(this CargoAd ad)
        {
            return $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            margin: 0;
            padding: 30px;
            background-color: #f0f2f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #212529;
        }}
        .card {{
            max-width: 640px;
            margin: auto;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
            overflow: hidden;
        }}
        .card-header {{
            background-color: #198754;
            color: #ffffff;
            padding: 20px 30px;
            font-size: 20px;
            font-weight: bold;
        }}
        .card-body {{
            padding: 30px;
        }}
        .info {{
            margin: 15px 0;
            font-size: 15px;
            line-height: 1.6;
        }}
        .info strong {{
            display: inline-block;
            width: 120px;
            color: #495057;
        }}
        .card-footer {{
            padding: 20px;
            text-align: center;
            font-size: 13px;
            color: #6c757d;
            background-color: #f8f9fa;
            border-top: 1px solid #e9ecef;
        }}
        @media (max-width: 600px) {{
            .card-body, .card-header, .card-footer {{
                padding: 20px;
            }}
            .info strong {{
                display: block;
                margin-bottom: 4px;
            }}
        }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='card-header'>Kargo İlanınız Güncellendi</div>
        <div class='card-body'>
            <div class='info'><strong>Başlık:</strong> {ad.Title}</div>
            <div class='info'><strong>Kargo Tipi:</strong> {ad.CargoType}</div>
            <div class='info'><strong>Ağırlık:</strong> {ad.Weight} kg</div>
            <div class='info'><strong>Nereden:</strong> {ad.PickCity}, {ad.PickCountry}</div>
            <div class='info'><strong>Nereye:</strong> {ad.DropCity}, {ad.DropCountry}</div>
            <div class='info'><strong>Fiyat:</strong> {ad.Price} {ad.currency}</div>
        </div>
        <div class='card-footer'>&copy; {DateTime.Now.Year} Logistify – Tüm hakları saklıdır.</div>
    </div>
</body>
</html>";
        }


        public static string ToCargoOfferMailBody(this CargoOffer offer)
        {
            return $@"
<!DOCTYPE html>
<html lang='tr'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            margin: 0;
            padding: 30px;
            background-color: #f0f2f5;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #212529;
        }}
        .card {{
            max-width: 640px;
            margin: auto;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
        }}
        .card-header {{
            background-color: #0d6efd;
            color: #ffffff;
            padding: 20px 30px;
            font-size: 20px;
            font-weight: bold;
        }}
        .card-body {{
            padding: 30px;
        }}
        .info {{
            margin: 15px 0;
            font-size: 15px;
            line-height: 1.6;
        }}
        .info strong {{
            display: inline-block;
            width: 140px;
            color: #495057;
        }}
        .card-footer {{
            padding: 20px;
            text-align: center;
            font-size: 13px;
            color: #6c757d;
            background-color: #f8f9fa;
            border-top: 1px solid #e9ecef;
        }}
        @media (max-width: 600px) {{
            .card-body, .card-header, .card-footer {{
                padding: 20px;
            }}
            .info strong {{
                display: block;
                margin-bottom: 4px;
            }}
        }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='card-header'>Kargo Teklifiniz Güncellendi</div>
        <div class='card-body'>
            <div class='info'><strong>İlan Başlığı:</strong> {offer.CargoAd.Title}</div>
            <div class='info'><strong>Teklif Mesajı:</strong> {offer.Message}</div>
            <div class='info'><strong>Fiyat:</strong> {offer.Price} {offer.CargoAd.currency}</div>
            <div class='info'><strong>Geçerlilik Tarihi:</strong> {offer.ExpiryDate?.ToString("dd.MM.yyyy") ?? "Belirtilmedi"}</div>
        </div>
        <div class='card-footer'>&copy; {DateTime.Now.Year} Logistify – Tüm hakları saklıdır.</div>
    </div>
</body>
</html>";
        }




    }
}
