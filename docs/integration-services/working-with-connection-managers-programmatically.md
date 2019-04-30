---
title: "Working with Connection Managers Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "connection managers [Integration Services], programming"
ms.assetid: 2686fe84-1ecc-48b8-9160-e7122274bd84
author: janinezhang
ms.author: janinez
manager: craigg
---
# Working with Connection Managers Programmatically

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  In [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], the AcquireConnection method of the associated connection manager class is the method that you call most often when you are working with connection managers in managed code. When you write managed code, you have to call the AcquireConnection method to use the functionality of a connection manager. You have to call this method regardless of whether you are writing managed code in a Script task, Script component, custom object, or custom application.  
  
 To call the AcquireConnection method successfully, you have to know the answers to the following questions:  
  
-   **Which connection managers return a managed object from the AcquireConnection method?**  
  
     Many connection managers return unmanaged COM objects (System.__ComObject) and these objects cannot easily be used from managed code. The list of these connection managers includes the frequently used OLE DB connection manager.  
  
-   **For those connection managers that return a managed object, what objects do their AcquireConnection methods return?**  
  
     To cast the return value to the appropriate type, you have to know what type of object the AcquireConnection method returns. For example, the AcquireConnection method for the [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection manager returns an open SqlConnection object when you use the SqlClient provider. However, the AcquireConnection method for the File connection manager just returns a string.  
  
 This topic answers these questions for the connection managers that are included with [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
## Connection Managers That Do Not Return a Managed Object  
 The following table lists the connection managers that return a native COM object (System.__ComObject) from the AcquireConnection method. These unmanaged objects cannot easily be used from managed code.  
  
|Connection Manager Type|Connection Manager Name|  
|-----------------------------|-----------------------------|  
|ADO|ADO Connection Manager|  
|MSOLAP90|[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Connection Manager|  
|EXCEL|Excel Connection Manager|  
|FTP|FTP Connection Manager|  
|HTTP|HTTP Connection Manager|  
|ODBC|ODBC Connection Manager|  
|OLEDB|OLE DB Connection Manager|  
  
 Typically, you can use an [!INCLUDE[vstecado](../includes/vstecado-md.md)] connection manager from managed code to connect to an ADO, Excel, ODBC, or OLE DB data source.  
  
## Return Values from the AcquireConnection Method  
 The following table lists the connection managers that return a managed object from the AcquireConnection method. These managed objects can easily be used from managed code.  
  
|Connection Manager Type|Connection Manager Name|Type of Return Value|Additional Information|  
|-----------------------------|-----------------------------|--------------------------|----------------------------|  
|[!INCLUDE[vstecado](../includes/vstecado-md.md)]|[!INCLUDE[vstecado](../includes/vstecado-md.md)] Connection Manager|**System.Data.SqlClient.SqlConnection**||  
|FILE|File Connection Manager|**System.String**|Path to the file.|  
|FLATFILE|Flat File Connection Manager|**System.String**|Path to the file.|  
|MSMQ|MSMQ Connection Manager|**System.Messaging.MessageQueue**||  
|MULTIFILE|Multiple Files Connection Manager|**System.String**|Path to one of the files.|  
|MULTIFLATFILE|Multiple Flat Files Connection Manager|**System.String**|Path to one of the files.|  
|SMOServer|SMO Connection Manager|**Microsoft.SqlServer.Management.Smo.Server**||  
|SMTP|SMTP Connection Manager|**System.String**|For example: `SmtpServer=<server name>;UseWindowsAuthentication=True;EnableSsl=False;`|  
|WMI|WMI Connection Manager|**System.Management.ManagementScope**||  
|SQLMOBILE|SQL Server Compact Connection Manager|**System.Data.SqlServerCe.SqlCeConnection**||  
  
## See Also  
 [Connecting to Data Sources in the Script Task](../integration-services/extending-packages-scripting/task/connecting-to-data-sources-in-the-script-task.md)   
 [Connecting to Data Sources in the Script Component](../integration-services/extending-packages-scripting/data-flow-script-component/connecting-to-data-sources-in-the-script-component.md)   
 [Connecting to Data Sources in a Custom Task](../integration-services/extending-packages-custom-objects/task/connecting-to-data-sources-in-a-custom-task.md)  
  
  
