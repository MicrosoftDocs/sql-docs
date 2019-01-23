---
title: "Step 4: Add a Flat File destination | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: f4088de3-16d8-419c-96a1-a2cd005d0a5b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 4-4: Add a Flat File destination

The error output of the Lookup Currency Key transformation redirects any data rows that failed the lookup to the Script transformation operation. To provide more information about the errors that occurred, the Script transformation runs a script that gets each error's description.  
  
In this task, you save all this information about the failed rows to a delimited text file for later processing. To save the failed rows, you add and configure a Flat File connection manager for the text file that contains the error data and a Flat File destination. By setting properties on the Flat File connection manager that the Flat File destination uses, you can specify how the Flat File destination formats and writes the text file. For more information, see [Flat File connection manager](../integration-services/connection-manager/flat-file-connection-manager.md) and [Flat File destination](../integration-services/data-flow/flat-file-destination.md).  
  
## Add and configure a Flat File destination  
  
1.  Select the **Data Flow** tab.  
  
2.  In the **SSIS Toolbox**, expand **Other Destinations**, and drag **Flat File Destination** onto the data flow design surface. Put the **Flat File Destination** directly underneath the **Get Error Description** transformation.  
  
3.  Select the **Get Error Description** transformation, and then drag the blue arrow onto the new **Flat File Destination**.  
  
4.  On the **Data Flow** design surface, select the name **Flat File Destination** in the new **Flat File Destination** transformation, and change that name to **Failed Rows**.  
  
5.  Right-click the **Failed Rows** transformation, select **Edit**, and then in the **Flat File Destination Editor**, select **New**.  
  
6.  In the **Flat File Format** dialog box, verify that **Delimited** is selected, and then select **OK**.  
  
7.  In the **Flat File Connection Manager Editor**, in the **Connection Manager Name** box enter *Error Data*.  
  
8.  In the **Flat File Connection Manager Editor** dialog box, select **Browse**, and locate the folder in which to store the file.  
  
9. In the **Open** dialog box, for **File name**, enter *ErrorOutput.txt*, and then select **Open**.  
  
10. In the **Flat File Connection Manager Editor** dialog box, verify that **Locale** is **English (United States)** and **Code page** is **1252 (ANSI-Latin I)**.  
  
11. In the options pane, select **Columns**.  
  
    In addition to the columns from the source data file, there are three new columns: ErrorCode, ErrorColumn, and ErrorDescription. These columns are the error output of the Lookup Currency Key transformation and the script in the Get Error Description transformation. You can use these columns to troubleshoot the cause of the failed row.  
  
12. Select **OK**.  
  
13. In the **Flat File Destination Editor**, clear the **Overwrite data in the file** check box.  
  
    Clearing this check box persists the errors over multiple package executions by appending each new run's error output.
  
14. In the **Flat File Destination Editor**, select **Mappings** to verify that all the columns are correct. Optionally, you can rename the columns in the destination.  
  
15. Select **OK**.  
  
## Go to next task
[Step 5: Test the Lesson 4 tutorial package](../integration-services/lesson-4-5-testing-the-lesson-4-tutorial-package.md)  
  
  
  
