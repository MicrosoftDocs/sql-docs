---
title: "Running the Address Book Sample Application | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "address book application scenario [ADO]"
  - "RDS scenarios [ADO]"
ms.assetid: 3a2644e9-d634-4ae6-a5b7-13fb7b317ec7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Running the Address Book Sample Application
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 To run the Address Book application, follow this procedure.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
### To run this application  
  
1.  Make sure that Microsoft SQL Server is running. Click **Start**, point to **Programs**, point to **Microsoft SQL Server 7.0**, and then click **Service Manager**. If there is a green arrow in the white circle, then SQL Server is running. If it is not (there will be a red square in the white circle), click **Start/Continue**.  
  
2.  In Microsoft Internet Explorer 4.0 or later, type the following address:  
  
     **https://** *webserver* **/RDS/AddressBook/AddrBook.asp**  
  
     where *webserver* is the name of the Web server where the RDS server components are installed.  
  
3.  You can then try various scenarios in the Address Book sample application, such as searching for a person based on his or her e-mail name, listing all people with the title "Program Manager," or editing existing records. Click **Find** to fill the data grid with all the available names.  
  
## See Also  
 [Address Book Data-Binding Object](../../../ado/guide/remote-data-service/address-book-data-binding-object.md)




