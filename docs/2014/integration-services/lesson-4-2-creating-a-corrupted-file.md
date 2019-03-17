---
title: "Step 2: Creating a Corrupted File | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: cd0b18dc-66c3-4d88-86ef-8e40cb660fae
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 2: Creating a Corrupted File
  In order to demonstrate the configuration and handling of transformation errors, you will have to create a sample flat file that when processed causes a component to fail.  
  
 In this task, you will create a copy of an existing sample flat file. You will then open the file in Notepad and edit the **CurrencyID** column to ensure that it will fail to produce a match during the transformations lookup. When the new file is processed, the lookup failure will cause the Currency Key Lookup transformation to fail and therefore fail the rest of the package. After you have created the corrupted sample file, you will run the package to view the package failure.  
  
### To create a corrupted sample flat file  
  
1.  In Notepad or any other text editor, open the Currency_VEB.txt file.  
  
     The sample data is included with the SSIS Lesson packages. To download the sample data and the lesson packages, do the following.  
  
    1.  Navigate to [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkID=267527).  
  
    2.  Click the **DOWNLOADS** tab.  
  
    3.  Click the SQL2012.Integration_Services.Create_Simple_ETL_Tutorial.Sample.zip file.  
  
2.  Use the text editor's find and replace feature to find all instances of `VEB` and replace them with `BAD`.  
  
3.  In the same folder as the other sample data files, save the modified file as `Currency_BAD.txt`.  
  
    > [!IMPORTANT]  
    >  Make sure that `Currency_BAD.txt` is saved the same folder as the other sample data files.  
  
4.  Close your text editor.  
  
### To verify that an error will occur during run time  
  
1.  On the **Debug** menu, click **Start Debugging**.  
  
     On the third iteration of the data flow, the Lookup Currency Key transformation tries to process the Currency_BAD.txt file, and the transformation will fail. The failure of the transformation will cause the whole package to fail.  
  
2.  On the **Debug** menu, click **Stop Debugging**.  
  
3.  On the design surface, click the **Execution Results** tab.  
  
4.  Browse through the log and verify that the following unhandled error occurred:  
  
     `[Lookup Currency Key[27]] Error: Row yielded no match during lookup.`  
  
    > [!NOTE]  
    >  The number 27 is the ID of the component. This value is assigned when you build the data flow, and the value in your package may be different.  
  
## Next Steps  
 [Step 3: Adding Error Flow Redirection](lesson-4-3-adding-error-flow-redirection.md)  
  
  
