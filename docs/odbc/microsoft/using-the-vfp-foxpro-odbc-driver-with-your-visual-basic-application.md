---
description: "Using the VFP FoxPro ODBC Driver with Your Visual Basic Application"
title: "Use the VFP FoxPro ODBC Driver with Your Visual Basic Application | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual FoxPro ODBC driver [ODBC], visual basic applications"
  - "Visual Basic applications [ODBC]"
  - "FoxPro ODBC driver [ODBC], visual basic applications"
  - "Visual FoxPro data [ODBC], visual basic applications"
ms.assetid: 5223ca23-5df6-4ebc-aa3b-70682ff27a8c
author: David-Engel
ms.author: v-davidengel
---
# Using the VFP FoxPro ODBC Driver with Your Visual Basic Application
Your Microsoft® Visual Basic® application can communicate with Visual FoxPro data by creating a data control that connects to a Visual FoxPro data source.  
  
#### To connect to Visual FoxPro data using the Data Control in Visual Basic  
  
1.  Create a data source named "test" that connects to the TasTrade sample database included in Visual FoxPro. The default Visual FoxPro installation places the TasTrade sample database in the location:  
  
    ```  
    c:\vfp\samples\mainsamp\data\tastrade.dbc  
    ```  
  
2.  In Visual Basic, create a new form and place a text box and a Data control on it.  
  
3.  Change the Data control's Connect property as follows:  
  
    ```  
    ODBC;DATABASE=tastrade;DSN=test  
    ```  
  
4.  Change the RecordsetType property to the following:  
  
    ```  
    2 - Snapshot  
    ```  
  
5.  Change the RecordSource property to the following:  
  
    ```  
    customer  
    ```  
  
6.  Change the DataSource property for the text box to the default name for the Data control to the following:  
  
    ```  
    data1  
    ```  
  
7.  Change the text box's DataField property to the following:  
  
    ```  
    customer_id  
    ```  
  
8.  Run the form, and use the Data control to skip through the customer id fields from the Visual FoxPro TasTrade sample database.
