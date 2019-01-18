---
title: "Creating Mailing Labels in Microsoft Word Using Visual FoxPro Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual FoxPro data [ODBC], mailing labels"
  - "Visual FoxPro data [ODBC], Word"
  - "mailing labels [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], Word"
  - "FoxPro ODBC driver [ODBC], word"
ms.assetid: c901b60c-9f84-407a-b3d1-b4d301a71370
author: MightyPen
ms.author: genemi
manager: craigg
---
# Creating Mailing Labels in Microsoft Word Using Visual FoxPro Data
You can use Visual FoxPro data in a Microsoft Word for Windows 95 or Windows 98 document. For example, you might want to create mailing labels from the customer information stored in a Visual FoxPro table.  
  
### To create mailing labels  
  
1.  In Microsoft Word, create a new blank document.  
  
2.  From the Tools menu, choose Mail Merge.  
  
3.  In the Mail Merge Helper, choose Create and then select Mailing Labels.  
  
4.  Under Main Document, choose Active Window.  
  
5.  Under Data Source, choose Get Data and then select Open Data Source.  
  
6.  In the Open Data Source dialog box, choose MS Query.  
  
7.  In the Select Data Source dialog box, select a Visual FoxPro data source and then click Use.  
  
8.  If the database accessed by your data source includes tables, select a table from the Add Tables dialog box. Microsoft Query displays the added table in the top half of the query designer.  
  
9. Select fields for your query by dragging them from the table onto the lower half of the designer.  
  
10. From the File menu, choose Return Data to Microsoft Word. Microsoft Query closes, and the data you selected is available for use in your mail merge document.  
  
11. Under Main Document, choose Setup.  
  
12. In the Label Options dialog box, select the printer and label information you want and then click OK.  
  
13. In the Create Labels dialog box, select the fields you want to print on the mailing labels and then click OK.  
  
14. In the Mail Merge Helper, under the Merge the Data with the Document, click Merge.  
  
15. In the Merge dialog box, select the options you want and then click Merge.
