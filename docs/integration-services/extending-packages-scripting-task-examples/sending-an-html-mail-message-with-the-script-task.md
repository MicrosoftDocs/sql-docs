---
title: "Sending an HTML Mail Message with the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Send Mail task [Integration Services]"
  - "Script task [Integration Services], examples"
  - "Script task [Integration Services], HTML mail message"
ms.assetid: dd2b1eef-b04f-4946-87ab-7bc56bb525ce
author: janinezhang
ms.author: janinez
manager: craigg
---
# Sending an HTML Mail Message with the Script Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] SendMail task only supports mail messages in plain text format. However you can easily send HTML mail messages by using the Script task and the mail capabilities of the .NET Framework.  
  
> [!NOTE]  
>  If you want to create a task that you can more easily reuse across multiple packages, consider using the code in this Script task sample as the starting point for a custom task. For more information, see [Developing a Custom Task](../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
## Description  
 The following example uses the **System.Net.Mail** namespace to configure and send an HTML mail message. The script obtains the To, From, Subject, and body of the e-mail from package variables, uses them to create a new **MailMessag**e, and sets its **IsBodyHtml** property to **True**. Then it obtains the SMTP server name from another package variable, initializes an instance of **System.Net.Mail.SmtpClient**, and calls its **Send** method to send the HTML message. The sample encapsulates the message sending functionality in a subroutine that could be reused in other scripts.  
  
#### To configure this Script Task example without an SMTP Connection Manager  
  
1.  Create string variables named `HtmlEmailTo`, `HtmlEmailFrom`, and `HtmlEmailSubject` and assign appropriate values to them for a valid test message.  
  
2.  Create a string variable named `HtmlEmailBody` and assign a string of HTML markup to it. For example:  
  
    ```  
    <html><body><h1>Testing</h1><p>This is a <b>test</b> message.</p></body></html>  
    ```  
  
3.  Create a string variable named `HtmlEmailServer` and assign the name of an available SMTP server that accepts anonymous outgoing messages.  
  
4.  Assign all five of these variables to the **ReadOnlyVariables** property of a new Script task.  
  
5.  Import the **System.Net** and **System.Net.Mail** namespaces into your code.  
  
 The sample code in this topic obtains the SMTP server name from a package variable. However, you could also take advantage of an SMTP connection manager to encapsulate the connection information, and extract the server name from the connection manager in your code. The <xref:Microsoft.SqlServer.Dts.ManagedConnections.SMTPConn.AcquireConnection%2A> method of the SMTP connection manager returns a string in the following format:  
  
 `SmtpServer=smtphost;UseWindowsAuthentication=False;EnableSsl=False;`  
  
 You can use the **String.Split** method to separate this argument list into an array of individual strings at each semicolon (;) or equal sign (=), and then extract the second argument (subscript 1) from the array as the server name.  
  
#### To configure this Script Task example with an SMTP Connection Manager  
  
1.  Modify the Script task configured earlier by removing the `HtmlEmailServer` variable from the list of **ReadOnlyVariables**.  
  
2.  Replace the line of code that obtains the server name:  
  
    ```  
    Dim smtpServer As String = _  
      Dts.Variables("HtmlEmailServer").Value.ToString  
    ```  
  
     with the following lines:  
  
    ```  
    Dim smtpConnectionString As String = _  
      DirectCast(Dts.Connections("SMTP Connection Manager").AcquireConnection(Dts.Transaction), String)  
    Dim smtpServer As String = _  
      smtpConnectionString.Split(New Char() {"="c, ";"c})(1)  
    ```  
  
## Code  
  
```vb  
Public Sub Main()  
  
  Dim htmlMessageFrom As String = _  
    Dts.Variables("HtmlEmailFrom").Value.ToString  
  Dim htmlMessageTo As String = _  
    Dts.Variables("HtmlEmailTo").Value.ToString  
  Dim htmlMessageSubject As String = _  
    Dts.Variables("HtmlEmailSubject").Value.ToString  
  Dim htmlMessageBody As String = _  
    Dts.Variables("HtmlEmailBody").Value.ToString  
  Dim smtpServer As String = _  
    Dts.Variables("HtmlEmailServer").Value.ToString  
  
  SendMailMessage( _  
      htmlMessageFrom, htmlMessageTo, _  
      htmlMessageSubject, htmlMessageBody, _  
      True, smtpServer)  
  
  Dts.TaskResult = ScriptResults.Success  
  
End Sub  
  
Private Sub SendMailMessage( _  
    ByVal From As String, ByVal SendTo As String,  _  
    ByVal Subject As String, ByVal Body As String, _  
    ByVal IsBodyHtml As Boolean, ByVal Server As String)  
  
  Dim htmlMessage As MailMessage  
  Dim mySmtpClient As SmtpClient  
  
  htmlMessage = New MailMessage( _  
    From, SendTo, Subject, Body)  
  htmlMessage.IsBodyHtml = IsBodyHtml  
  
  mySmtpClient = New SmtpClient(Server)  
  mySmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials  
  mySmtpClient.Send(htmlMessage)  
  
End Sub  
```  
  
```csharp  
public void Main()  
        {  
  
            string htmlMessageFrom = Dts.Variables["HtmlEmailFrom"].Value.ToString();  
            string htmlMessageTo = Dts.Variables["HtmlEmailTo"].Value.ToString();  
            string htmlMessageSubject = Dts.Variables["HtmlEmailSubject"].Value.ToString();  
            string htmlMessageBody = Dts.Variables["HtmlEmailBody"].Value.ToString();  
            string smtpServer = Dts.Variables["HtmlEmailServer"].Value.ToString();  
  
            SendMailMessage(htmlMessageFrom, htmlMessageTo, htmlMessageSubject, htmlMessageBody, true, smtpServer);  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
  
        }  
  
        private void SendMailMessage(string From, string SendTo, string Subject, string Body, bool IsBodyHtml, string Server)  
        {  
  
            MailMessage htmlMessage;  
            SmtpClient mySmtpClient;  
  
            htmlMessage = new MailMessage(From, SendTo, Subject, Body);  
            htmlMessage.IsBodyHtml = IsBodyHtml;  
  
            mySmtpClient = new SmtpClient(Server);  
            mySmtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;  
            mySmtpClient.Send(htmlMessage);  
  
        }  
```  
  
## See Also  
 [Send Mail Task](../../integration-services/control-flow/send-mail-task.md)  
  
  
