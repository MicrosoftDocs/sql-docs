---
title: "Querying and Updating Visual FoxPro Data from Microsoft Access | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "querying Visual FoxPro data [ODBC]"
  - "FoxPro ODBC driver [ODBC], Access"
  - "Visual FoxPro data [ODBC], Access"
  - "Visual FoxPro ODBC driver [ODBC], Access"
  - "Visual FoxPro data [ODBC], querying and updating"
  - "updating Visual FoxPro data [ODBC]"
ms.assetid: 2d314e78-9edf-44b2-bd8b-96784236bcbe
author: MightyPen
ms.author: genemi
manager: craigg
---
# Querying and Updating Visual FoxPro Data from Microsoft Access
You can query and update data stored in a Visual FoxPro database from a Microsoft Access database by using the Link Table option.  
  
### To link a Visual FoxPro database to a Microsoft Access database  
  
1.  Open a Microsoft Access database.  
  
2.  From the Tables tab, click New.  
  
3.  In the New Table dialog box, select Link Table and click OK.  
  
4.  In the Link Dialog box, select ODBC Database in the Files of type list.  
  
5.  In the SQL Data Sources dialog box, select the data source that connects to the Visual FoxPro data you want to query and click OK.  
  
6.  In the Link Tables dialog box, select the tables you want to query and update and click OK. The linked Visual FoxPro tables are displayed in the Tables tab of the Microsoft Access database.  
  
 You can now use Microsoft Access to query and update data in the linked Visual FoxPro tables. Changes you make to linked data are sent back to the Visual FoxPro data source.  
  
 If you do not want changes you make in Microsoft Access to affect the data on the Visual FoxPro data source, see [Importing Visual FoxPro Data into Microsoft Access](../../odbc/microsoft/importing-visual-foxpro-data-into-microsoft-access.md).
