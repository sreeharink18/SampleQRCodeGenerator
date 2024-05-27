using Microsoft.AspNetCore.Mvc;
using QRCoder;
using SampleQRCodeGenerator.Models;
using static QRCoder.PayloadGenerator;

namespace SampleQRCodeGenerator.Controllers
{
    public class BarCodeController : Controller
    {
        public IActionResult Index()
        {
            QRCodeModel model = new();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(QRCodeModel model)
        {
            Payload? payload = null;
            switch (model.QRCodeType)
            {
                case 1: // compose sms
                    payload = new SMS(model.SMSPhoneNumber, model.SMSBody);
                    break;
                case 2: // compose whatsapp message
                    payload = new WhatsAppMessage(model.WhatsAppNumber, model.WhatsAppMessage);
                    break;
                case 3: //compose email
                    payload = new Mail(model.ReceiverEmailAddress, model.EmailSubject, model.EmailMessage);
                    break;
                case 4: // wifi qr code
                    payload = new WiFi(model.WIFIName, model.WIFIPassword, WiFi.Authentication.WPA);
                    break;
                case 5:
                    Doctor doc = new Doctor(model.DocId, model.DocName);
                    string docDetail = doc.ToString();
                    QRCodeGenerator qRCodeGenerator = new();
                    QRCodeData qrCodeDat = qRCodeGenerator.CreateQrCode(docDetail,
                    QRCodeGenerator.ECCLevel.Q);
                    BitmapByteQRCode qrCod = new(qrCodeDat);
                    string base64Strin = Convert.ToBase64String(qrCod.GetGraphic(20));
                    model.QRImageURL = "data:image/png;base64," + base64Strin;
                    return View("Index", model);

            }

            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload);
            BitmapByteQRCode qrCode = new(qrCodeData);
            string base64String = Convert.ToBase64String(qrCode.GetGraphic(20));
            model.QRImageURL = "data:image/png;base64," + base64String;
            return View("Index", model);

        }
    }


}
