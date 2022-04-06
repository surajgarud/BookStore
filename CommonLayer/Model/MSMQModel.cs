using com.sun.org.apache.xerces.@internal.impl.msg;
using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void send(string token)
        {
            this.messageQueue.Path = @".\private$\Tokens";
            try
            {
                if (!MessageQueue.Exists(this.messageQueue.Path))
                {
                    MessageQueue.Create(this.messageQueue.Path);

                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                this.messageQueue.Send(token);
                this.messageQueue.BeginReceive();
                this.messageQueue.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = this.messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailmessege = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("surajgarud49@gmail.com", "Surajgarud@49"),
                    EnableSsl = true
                };
                mailmessege.From = new MailAddress("surajgarud49@gmail.com");
                mailmessege.To.Add(new MailAddress("surajgarud49@gmail.com"));
                mailmessege.Body = token;
                mailmessege.Subject = "Project reset Link";
                smtpClient.Send(mailmessege);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
