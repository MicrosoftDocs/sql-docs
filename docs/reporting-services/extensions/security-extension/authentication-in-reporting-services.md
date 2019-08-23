---
title: "Authentication in Reporting Services | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "security [Reporting Services], authentication"
  - "forms-based authentication [Reporting Services]"
  - "authentication [Reporting Services]"
  - "custom authentication [Reporting Services]"
ms.assetid: 103ce1f9-31d8-44bb-b540-2752e4dcf60b
author: maggiesMSFT
ms.author: maggies
---
# Authentication in Reporting Services
  Authentication is the process of establishing a user's right to an identity. There are many techniques that you can use to authenticate a user. The most common way is to use passwords. When you implement Forms Authentication, for example, you want an implementation that queries users for credentials (usually by some interface that requests a login name and password) and then validates users against a data store, such as a database table or configuration file. If the credentials can't be validated, the authentication process fails and the user will assume an anonymous identity.  
  
## Custom Authentication in Reporting Services  
 In [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], the Windows operating system handles the authentication of users either through integrated security or through the explicit reception and validation of user credentials. Custom authentication can be developed in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] to support additional authentication schemes. This is made possible through the security extension interface <xref:Microsoft.ReportingServices.Interfaces.IAuthenticationExtension2>. All extensions inherit from the <xref:Microsoft.ReportingServices.Interfaces.IExtension> base interface for any extension deployed and used by the report server. <xref:Microsoft.ReportingServices.Interfaces.IExtension>, as well as <xref:Microsoft.ReportingServices.Interfaces.IAuthenticationExtension2>, are members of the <xref:Microsoft.ReportingServices.Interfaces> namespace.  
  
 The primary way to authenticate against a report server in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] is the <xref:ReportService2010.ReportingService2010.LogonUser%2A> method. This member of the Reporting Services Web service can be used to pass user credentials to a report server for validation. Your underlying security extension implements **IAuthenticationExtension2.LogonUser** which contains your custom authentication code. In the Forms Authentication sample, **LogonUser**, which performs an authentication check against the supplied credentials and a custom user store in a database. An example of an implementation of **LogonUser** looks like this:  
  
```  
public bool LogonUser(string userName, string password, string authority)  
{  
   return AuthenticationUtilities.VerifyPassword(userName, password);  
}  
  
```  
  
 The following sample function is used to verify the supplied credentials:  
  
```  
  
internal static bool VerifyPassword(string suppliedUserName,  
   string suppliedPassword)  
{   
   bool passwordMatch = false;  
   // Get the salt and pwd from the database based on the user name.  
   // See "How To: Use DPAPI (Machine Store) from ASP.NET," "How To:  
   // Use DPAPI (User Store) from Enterprise Services," and "How To:  
   // Create a DPAPI Library" for more information about how to use  
   // DPAPI to securely store connection strings.  
   SqlConnection conn = new SqlConnection(  
      "Server=localhost;" +   
      "Integrated Security=SSPI;" +  
      "database=UserAccounts");  
   SqlCommand cmd = new SqlCommand("LookupUser", conn);  
   cmd.CommandType = CommandType.StoredProcedure;  
  
   SqlParameter sqlParam = cmd.Parameters.Add("@userName",  
       SqlDbType.VarChar,  
       255);  
   sqlParam.Value = suppliedUserName;  
   try  
   {  
      conn.Open();  
      SqlDataReader reader = cmd.ExecuteReader();  
      reader.Read(); // Advance to the one and only row  
      // Return output parameters from returned data stream  
      string dbPasswordHash = reader.GetString(0);  
      string salt = reader.GetString(1);  
      reader.Close();  
      // Now take the salt and the password entered by the user  
      // and concatenate them together.  
      string passwordAndSalt = String.Concat(suppliedPassword, salt);  
      // Now hash them  
      string hashedPasswordAndSalt =  
         FormsAuthentication.HashPasswordForStoringInConfigFile(  
         passwordAndSalt,  
         "SHA1");  
      // Now verify them. Returns true if they are equal.  
      passwordMatch = hashedPasswordAndSalt.Equals(dbPasswordHash);  
   }  
   catch (Exception ex)  
   {  
       throw new Exception("Exception verifying password. " +  
          ex.Message);  
   }  
   finally  
   {  
       conn.Close();  
   }  
   return passwordMatch;  
}  
```  
  
## Authentication Flow  
 The Reporting Services Web service provides custom authentication extensions to enable Forms Authentication by the web portal and the report server.  
  
 The <xref:ReportService2010.ReportingService2010.LogonUser%2A> method of the Reporting Services Web service is used to submit credentials to the report server for authentication. The Web service uses HTTP headers to pass an authentication ticket (known as a "cookie") from the server to the client for validated logon requests.  
  
 The following illustration depicts the method of authenticating users to the Web service when your application is deployed with a report server configured to use a custom authentication extension.  
  
 ![Reporting Services security authentication flow](../../../reporting-services/extensions/security-extension/media/rosettasecurityextensionauthenticationflow.gif "Reporting Services security authentication flow")  
  
 As shown in Figure 2, the authentication process is as follows:  
  
1.  A client application calls the Web service <xref:ReportService2010.ReportingService2010.LogonUser%2A> method to authenticate a user.  
  
2.  The Web service makes a call to the <xref:ReportService2010.ReportingService2010.LogonUser%2A> method of your security extension, specifically, the class that implements **IAuthenticationExtension2**.  
  
3.  Your implementation of <xref:ReportService2010.ReportingService2010.LogonUser%2A> validates the user name and password in the user store or security authority.  
  
4.  Upon successful authentication, the Web service creates a cookie and manages it for the session.  
  
5.  The Web service returns the authentication ticket to the calling application on the HTTP header.  
  
 When the Web service successfully authenticates a user through the security extension, it generates a cookie that is used for subsequent requests. The cookie may not persist within the custom security authority because the report server does not own the security authority. The cookie is returned from the <xref:ReportService2010.ReportingService2010.LogonUser%2A> Web service method and is used in subsequent Web service method calls and in URL access.  
  
> [!NOTE]  
>  In order to avoid compromising the cookie during transmission, authentication cookies returned from <xref:ReportService2010.ReportingService2010.LogonUser%2A> should be transmitted securely using Secure Sockets Layer (SSL) encryption.  
  
 If you access the report server through URL access when a custom security extension is installed, Internet Information Services (IIS) and [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] automatically manage the transmission of the authentication ticket. If you are accessing the report server through the SOAP API, your implementation of the proxy class must include additional support for managing the authentication ticket. For more information about using the SOAP API and managing the authentication ticket, see "Using the Web Service with Custom Security."  
  
## Forms Authentication  
 Forms Authentication is a type of [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] authentication in which an unauthenticated user is directed to an HTML form. Once the user provides credentials, the system issues a cookie containing an authentication ticket. On later requests, the system first checks the cookie to see if the user was already authenticated by the report server.  
  
 [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] can be extended to support Forms Authentication using the security extensibility interfaces available through the Reporting Services API. If you extend [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] to use Forms Authentication, use Secure Sockets Layer (SSL) for all communications with the report server to prevent malicious users from gaining access to another user's cookie. SSL enables clients and a report server to authenticate each other and to ensure that no other computers can read the contents of communications between the two computers. All data sent from a client through an SSL connection is encrypted so that malicious users cannot intercept passwords or data sent to a report server.  
  
 Forms Authentication is generally implemented to support accounts and authentication for platforms other than Windows. A graphical interface is presented to a user who requests access to a report server, and the supplied credentials are submitted to a security authority for authentication.  
  
 Forms Authentication requires that a person is present to enter credentials. For unattended applications that communicate directly with the Reporting Services Web service, Forms Authentication must be combined with a custom authentication scheme.  
  
 Forms Authentication is appropriate for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] when:  
  
-   You need to store and authenticate users that do not have [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows accounts, and  
  
-   You need to provide your own user interface form as a logon page between different pages on a Web site.  
  
 Consider the following when writing a custom security extension that supports Forms Authentication:  
  
-   If you use Forms Authentication, anonymous access must be enabled on the report server virtual directory in Internet Information Services (IIS).  
  
-   [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] authentication must be set to Forms. You configure [!INCLUDE[vstecasp](../../../includes/vstecasp-md.md)] authentication in the Web.config file for the report server.  
  
-   [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] can authenticate and authorize users with either Windows Authentication or custom authentication, but not both. [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] does not support simultaneous use of multiple security extensions.  
  
## See Also  
 [Implementing a Security Extension](../../../reporting-services/extensions/security-extension/implementing-a-security-extension.md)  
  
  
