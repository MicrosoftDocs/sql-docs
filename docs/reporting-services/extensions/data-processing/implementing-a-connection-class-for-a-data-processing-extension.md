---
title: "Implementing a Connection Class for a Data Processing Extension | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: extensions


ms.topic: reference
helpviewer_keywords: 
  - "connections [Reporting Services], data processing extensions"
  - "Connection class"
  - "data processing extensions [Reporting Services], connections"
ms.assetid: 7047d29e-a2c9-4e6f-ad02-635851a38ed7
author: markingmyname
ms.author: maghan
---
# Implementing a Connection Class for a Data Processing Extension
  The **Connection** object represents a database connection or similar resource and is the starting point for users of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extension. It represents connections to database servers, though any entity with similar behavior can be exposed as a **Connection**.  
  
 To implement a **Connection** object, create a class that implements <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection> and optionally implements <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension>.  
  
 In your implementation, you must ensure that a connection is created and opened before commands can be executed. Ensure that your implementation requires clients to open and close connections explicitly, rather than having your implementation open and close connections implicitly for the client. Perform your security checks when the connection is obtained. Requiring an existing connection for the other classes in your [!INCLUDE[ssRS](../../../includes/ssrs.md)] data processing extension will then ensure that security checks are always performed when working with your data source.  
  
 The properties of the desired connection are represented as a connection string. It is strongly recommended that [!INCLUDE[ssRS](../../../includes/ssrs.md)] data processing extensions support the <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection.ConnectionString%2A> property using the familiar name/value pair system defined by OLE DB.  
  
> [!NOTE]  
>  **Connection** objects are often resource-intensive to obtain, so you may want to consider pooling connections or other techniques to mitigate this.  
  
 <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection> inherits from <xref:Microsoft.ReportingServices.Interfaces.IExtension>. You must implement the <xref:Microsoft.ReportingServices.Interfaces.IExtension> interface as part of your connection class implementation. The <xref:Microsoft.ReportingServices.Interfaces.IExtension> interface enables a class to implement a localized extension name and to process extension-specific configuration information stored in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration file.  
  
 Your **Connection** object contains the <xref:Microsoft.ReportingServices.Interfaces.IExtension.LocalizedName%2A> property through its implementation of <xref:Microsoft.ReportingServices.Interfaces.IExtension>. It is strongly recommended that [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] data processing extensions support the <xref:Microsoft.ReportingServices.Interfaces.IExtension.LocalizedName%2A> property, so that users encounter a familiar, localized name for the extension in a user interface, such as Report Manager.  
  
 <xref:Microsoft.ReportingServices.Interfaces.IExtension> also enables your **Connection** object to retrieve and process custom configuration data stored in the RSReportServer.config file. For more information about processing custom configuration data, see the <xref:Microsoft.ReportingServices.Interfaces.IExtension.SetConfiguration%2A> method.  
  
 The class that implements <xref:Microsoft.ReportingServices.Interfaces.IExtension> is not unloaded from memory when the rest of your data processing extension classes are unloaded. Because of this, you can use your **Extension** class to store cross-connection state information or to store data that can be cached in memory. Your **Extension** class remains in memory as long as the report server is running.  
  
 You can extend your **Connection** class to include support for credentials in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] by implementing <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension>. When you implement the <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension.IntegratedSecurity%2A>, <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension.UserName%2A>, and <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension.Password%2A> properties of the <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension> interface, you enable the **Integrated Security** check box and **Username** and **Password** text boxes of the **Data Source** dialog in Report Designer. This enables Report Designer to store and retrieve credentials for data sources that support authentication. The credentials are stored securely and used when rendering reports in preview mode.  
  
> [!NOTE]  
>  Implementing <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension> implicitly requires you to implement the members of the <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection> and <xref:Microsoft.ReportingServices.Interfaces.IExtension> interfaces.  
>   
>  For a sample **Connection** class implementation, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Reporting Services Extensions](../../../reporting-services/extensions/reporting-services-extensions.md)   
 [Implementing a Data Processing Extension](../../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)   
 [Reporting Services Extension Library](../../../reporting-services/extensions/reporting-services-extension-library.md)  
  
  
