---
title: "RDS Scenario | Microsoft Docs"
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
ms.assetid: a7dcad87-aaf0-4b02-9660-472f8469761c
author: MightyPen
ms.author: genemi
manager: craigg
---
# RDS Scenario
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 The Address Book application is a scenario that shows you how to use Remote Data Service (RDS) to build a simple, data-aware Web application - an online corporate address book. This scenario is useful for Microsoft Visual Basic Scripting Edition (VBScript) and COM programmers who want to learn how to use data-aware ActiveX Controls with RDS, and for more experienced software developers who want to build data-centric Web applications.  
  
 This scenario assumes that you know how to use basic HTML layout tags, use DHTML data binding techniques, and program with ActiveX Controls.  
  
 If you have installed the SDK, the complete source code for the Address Book sample application can be found in the SDK directory at samples\dataaccess\rds\AddressBook\AddressBook.asp. To view the Address Book scenario, in Internet Explorer 4.0 or later, type **https://*webserver*/RDS/AddressBook/AddressBook.asp** where *webserver* is the name given to your Windows NT 4.0 or Windows 2000 Web server computer that is running Internet Information Services (IIS) and ASP.  
  
## Introduction to Address Book  
 The Address Book sample application provides a simple online address book that you can use to publish a searchable directory over an intranet. The address book is designed so that a user can enter a search string in one or more fields to request information about employees. To show you the basic features of Remote Data Service, the sample application is intentionally kept small, with a minimum number of objects and search fields.  
  
 The application interface consists of the following parts:  
  
-   A non-visual **RDS.DataControl** data-binding object that is used by the client to connect to the database.  
  
-   HTML text boxes that act as input fields for employee attribute search criteria.  
  
-   HTML command buttons to build queries, clear search fields, update the database with employee information, cancel pending changes, and navigate the rows of data displayed in the grid.  
  
-   DHTML data binding to display data returned from queries against a back-end database (through the **RDS.DataControl** data-binding object) in a table.  
  
-   VBScript routines that connect each of the elements that were previously mentioned and allow them to interact. VBScript code is also used to initialize the **RDS.DataControl** object and dynamically create the column headings in the HTML table from the names of the **RDS.DataControl** recordset fields.  
  
 Follow the links from step to step to set up and run the scenario and to learn more about how the scenario works.  
  
 This scenario contains the following topics.  
  
-   [System Requirements for the Address Book Application](../../../ado/guide/remote-data-service/system-requirements-for-the-address-book-application.md)  
  
-   [Running the Address Book SQL Script](../../../ado/guide/remote-data-service/running-the-address-book-sql-script.md)  
  
-   [Running the Address Book Sample Application](../../../ado/guide/remote-data-service/running-the-address-book-sample-application.md)  
  
-   [Address Book Data-Binding Object](../../../ado/guide/remote-data-service/address-book-data-binding-object.md)  
  
-   [Address Book Command Buttons](../../../ado/guide/remote-data-service/address-book-command-buttons.md)  
  
-   [Address Book Navigation Buttons](../../../ado/guide/remote-data-service/address-book-navigation-buttons.md)  
  
## See Also  
 [System Requirements for the Address Book Application](../../../ado/guide/remote-data-service/system-requirements-for-the-address-book-application.md)   
 [Microsoft ActiveX Data Objects (ADO)](../../../ado/microsoft-activex-data-objects-ado.md)   
 [RDS Fundamentals](../../../ado/guide/remote-data-service/rds-fundamentals.md)   
 [RDS Tutorial](../../../ado/guide/remote-data-service/rds-tutorial.md)


