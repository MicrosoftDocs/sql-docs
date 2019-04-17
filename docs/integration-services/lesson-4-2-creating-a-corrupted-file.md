---
title: "Step 2: Create a corrupted file | Microsoft Docs"
ms.custom: ""
ms.date: "01/07/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: cd0b18dc-66c3-4d88-86ef-8e40cb660fae
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 4-2: Create a corrupted file

To demonstrate the configuration and handling of transformation errors, you need a sample flat file that, when processed, causes a component to fail.  
  
In this task, you create a copy of an existing sample flat file. You then open the file in Notepad and edit the **CurrencyID** column to contain an erroneous value, which fails the lookup. When the corrupted file is processed, the lookup failure causes the Currency Key Lookup transformation to fail and therefore fail the rest of the package. After you've created the corrupted sample file, you run the package to view the package failure.  
  
## Create a corrupted sample flat file  
  
1.  In Notepad or any other text editor, open the **Currency_VEB.txt** file.  
  
2.  Use the text editor's find and replace feature to find all instances of **VEB** and replace them with **BAD**.  
  
3.  In the same folder as the other sample data files, save the modified file as **Currency_BAD.txt**.  
  
    > [!NOTE]  
    > Make sure that you save **Currency_BAD.txt** in the same folder as the other sample data files.  
  
4.  Close your text editor.  
  
## Verify that an error occurs during run time  
  
1.  On the **Debug** menu, select **Start Debugging**.  
  
    On the third iteration of the data flow, the Lookup Currency Key transformation tries to process the **Currency_BAD.txt** file, and the transformation fails. The failure of the transformation causes the whole package to fail.  
  
2.  On the **Debug** menu, select **Stop Debugging**.  
  
3.  On the design surface, select the **Execution Results** tab.  
  
4.  Browse through the log and verify that the following unhandled error occurred:  
  
    ```
    [Lookup Currency Key[27]] Error: Row yielded no match during lookup.
    ```
  
    > [!NOTE]  
    > The number 27 is the ID of the component. This value is assigned when you build the data flow, and the value in your package may be different.  
  
## Go to next task  
[Step 3: Add error flow redirection](../integration-services/lesson-4-3-adding-error-flow-redirection.md)  
  
  
  
