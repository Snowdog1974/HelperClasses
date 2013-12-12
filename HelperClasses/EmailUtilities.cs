using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelperClasses
{
    public class Email :  INotifyPropertyChanged
    {

        private string sender;
        public string Sender
        {
            get { return sender; }
            set
            {
                sender = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sender"));
            }
        }

        private string subject;
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Subject"));
            }
        }

        private string recepient;
        public string Recepient
        {
            get { return recepient; }
            set
            {
                recepient = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Recepient"));
            }
        }

        private string messageTxt;
        public string MessageTxt
        {
            get { return messageTxt; }
            set
            {
                messageTxt = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MessageTxt"));
            }
        }


        private string attachment;
        public string Attachment
        {
            get { return attachment; }
            set
            {
                attachment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Attachment"));
            }
        }


        private string smtpHost;
        public string SmtpHost
        {
            get { return smtpHost; }
            set
            {
                smtpHost = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SmtpHost"));
            }
        }

        private int timeout;
        public int Timeout
        {
            get { return timeout; }
            set
            {
                timeout = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Timeout"));
            }
        }

        private string gmailAddress;
        public string GmailAddress
        {
            get { return gmailAddress; }
            set
            {
                gmailAddress = value;
                OnPropertyChanged(new PropertyChangedEventArgs("GmailAddress"));
            }
        }

        private string gmailPwd;
        public string GmailPwd
        {
            get { return gmailPwd; }
            set
            {
                gmailPwd = value;
                OnPropertyChanged(new PropertyChangedEventArgs("GmailPwd"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        public Email(string sender, string recepient, string subject, string messageTxt, string attachment, string smtphost, string gmailAddress, string gmailPwd, int timeOut)
        {
            Sender = sender;
            Recepient = recepient;
            Subject = subject;
            MessageTxt = messageTxt;
            Attachment = attachment;
            SmtpHost = smtpHost;
            GmailAddress = gmailAddress;
            GmailPwd = gmailPwd;
            Timeout = timeOut;
        }
        /// <summary>
        /// If using Smtp-server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recepient"></param>
        /// <param name="subject"></param>
        /// <param name="messageTxt"></param>
        /// <param name="attachment"></param>
        /// <param name="smtphost"></param>
        public Email(string sender, string recepient, string subject, string messageTxt, string attachment, string smtphost)
        {
            Sender = sender;
            Recepient = recepient;
            Subject = subject;
            MessageTxt = messageTxt;
            Attachment = attachment;
            SmtpHost = smtpHost;

        }

        /// <summary>
        /// If using G-mail
        /// </summary>
        /// <param name="recepient"></param>
        /// <param name="subject"></param>
        /// <param name="messageTxt"></param>
        /// <param name="attachment"></param>
        /// <param name="gmailAddress"></param>
        /// <param name="gmailPwd"></param>
        public Email(string recepient, string subject, string messageTxt, string attachment, string gmailAddress, string gmailPwd, int timeOut)
        {
            Recepient = recepient;
            Subject = subject;
            MessageTxt = messageTxt;
            Attachment = attachment;
            GmailAddress = gmailAddress;
            GmailPwd = gmailPwd;
            Timeout = timeOut;
        }

        public Email(string sender, string recepient, string subject, string messageTxt, string attachment, string gmailAddress, string gmailPwd)
        {
            Recepient = recepient;
            Subject = subject;
            MessageTxt = messageTxt;
            Attachment = attachment;
            GmailAddress = gmailAddress;
            GmailPwd = gmailPwd;
            Timeout = 50000;
        }


        public bool SendMailWithAttachment()
        {
            bool result = false;
            try
            {

                if (SmtpHost != null && SmtpHost != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(Recepient);
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(Sender);
                    message.Body = MessageTxt;
                    message.Attachments.Add(new System.Net.Mail.Attachment(Attachment));
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost);
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;



        }

        public bool SendMailWithAttachment(List<string> recepientList)
        {
            bool result = false;
            try
            {

                if (SmtpHost != null && SmtpHost != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    foreach (string item in recepientList)
                    {
                        message.To.Add(item);
                    }

                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(Sender);
                    message.Body = MessageTxt;
                    message.Attachments.Add(new System.Net.Mail.Attachment(Attachment));
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost);
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;



        }

        public bool SendMailWithAttachmentList(List<string> recepientList, List<string> attachmentList)
        {
            bool result = false;
            try
            {

                if (SmtpHost != null && SmtpHost != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    foreach (string item in recepientList)
                    {
                        message.To.Add(item);
                    }

                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(Sender);
                    message.Body = MessageTxt;
                    foreach (string item in attachmentList)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost);
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;




        }

        public bool SendMailWithAttachmentList(List<string> attachmentList)
        {

            bool result = false;
            try
            {

                if (SmtpHost != null && SmtpHost != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(Recepient);
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(Sender);
                    message.Body = MessageTxt;
                    foreach (string item in attachmentList)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost);
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;



        }


        public bool SendMail()
        {
            bool result = false;
            try
            {

                if (SmtpHost != null && SmtpHost != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(Recepient);
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(Sender);
                    message.Body = MessageTxt;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost);
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;



        }

        public bool SendMail(List<string> recepientList)
        {
            bool result = false;
            try
            {
                if (SmtpHost != null && SmtpHost != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    foreach (string item in recepientList)
                    {
                        message.To.Add(item);
                    }

                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(Sender);
                    message.Body = MessageTxt;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(SmtpHost);
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;



        }

        //************************************* using Gmail******************************************


        public bool SendGMailWithAttachment()
        {
            bool result = false;
            try
            {

                if (GmailPwd != null && GmailPwd != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(Recepient);
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(GmailAddress);
                    message.Body = MessageTxt;
                    message.Attachments.Add(new System.Net.Mail.Attachment(Attachment));

                    System.Net.Mail.SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(message.From.Address, GmailPwd),
                        Timeout = 20000
                    };
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;


        }

        public bool SendGMailWithAttachment(List<string> recepientList)
        {
            bool result = false;
            try
            {
                if (GmailPwd != null && GmailPwd != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    foreach (string item in recepientList)
                    {
                        message.To.Add(item);
                    }

                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(GmailAddress);
                    message.Body = MessageTxt;
                    message.Attachments.Add(new System.Net.Mail.Attachment(Attachment));

                    System.Net.Mail.SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(message.From.Address, GmailPwd),
                        Timeout = 20000
                    };
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;





        }

        public bool SendGMailWithAttachmentList(List<string> recepientList, List<string> attachmentList)
        {
            bool result = false;
            try
            {
                if (GmailPwd != null && GmailPwd != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    foreach (string item in recepientList)
                    {
                        message.To.Add(item);
                    }

                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(GmailAddress);
                    message.Body = MessageTxt;
                    foreach (string item in attachmentList)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }


                    System.Net.Mail.SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(message.From.Address, GmailPwd),
                        Timeout = 20000
                    };
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;




        }

        public bool SendGMailWithAttachmentList(List<string> attachmentList)
        {
            bool result = false;
            try
            {
                if (GmailPwd != null && GmailPwd != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(Recepient);
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(GmailAddress);
                    message.Body = MessageTxt;
                    foreach (string item in attachmentList)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }

                    System.Net.Mail.SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(message.From.Address, GmailPwd),
                        Timeout = 20000
                    };
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;

        }


        public bool SendGMail()
        {
            bool result = false;
            try
            {
                if (GmailPwd != null && GmailPwd != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.To.Add(Recepient);
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(GmailAddress);
                    message.Body = MessageTxt;


                    System.Net.Mail.SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(message.From.Address, GmailPwd),
                        Timeout = 20000
                    };
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;


        }

        public bool SendGMail(List<string> recepientList)
        {
            bool result = false;
            try
            {
                if (GmailPwd != null && GmailPwd != "")
                {
                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    foreach (string item in recepientList)
                    {
                        message.To.Add(item);
                    }
                    message.Subject = Subject;
                    message.From = new System.Net.Mail.MailAddress(GmailAddress);
                    message.Body = MessageTxt;


                    System.Net.Mail.SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(message.From.Address, GmailPwd),
                        Timeout = 20000
                    };
                    smtp.Send(message);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {

                //ErrorLog error = new ErrorLog(0, DateTime.Now, 0, "Error", e.Message, Environment.StackTrace, 0, 0, Environment.MachineName);
                //IErrorHandling.WriteToLog(error);
            }
            return result;



        }






    }

    public class EmailUtilities
    {
        bool invalid = false;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

    }
}
