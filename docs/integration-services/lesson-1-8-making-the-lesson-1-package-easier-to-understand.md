---
title: "Step 8: Annotate and format the Lesson 1 package | Microsoft Docs"
ms.custom: ""
ms.date: "01/03/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: e3751e53-77c7-47d0-8fe8-73ed1a53413a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-8: Annotate and format the Lesson 1 package 

Now that you've completed the configuration of the Lesson 1 package, it's probably time to tidy up the package layout. If the shapes in the control and data flow layouts are different sizes, or not laid out evenly, the package may be more difficult to understand.  
  
[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools provides tools to easily format the package layout. The formatting features include the ability to make shapes the same size, align shapes, and change the horizontal and vertical spacing between shapes.  
  
Another way to improve the understanding of package functionality is to add annotations that describe package functionality.  
  
In this task, you use the formatting features in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools to improve the layout of the data flow and also to add an annotation.  
  
## Format the layout of the data flow  
  
1.  If the Lesson 1 package isn't already open, double-click **Lesson 1.dtsx** in **Solution Explorer**.  
  
2.  Select the **Data Flow** tab.  
  
3.  To select all the data flow components at once, use **Edit** > **Select All**.
  
4.  On the **Format** menu, select **Make Same Size**, and then select **Both**.  
  
5.  With the data flow objects selected, on the **Format** menu, select **Align**, and then select **Centers**.  

6.  With the data flow objects selected, on the **Format** menu, point to **Vertical Spacing**, and then select **Make Equal**.  
  
## Add an annotation to the data flow  
  
1.  Right-click anywhere in the background of the data flow design surface and then select **Add Annotation**.  
  
2.  Enter or paste the following text in the annotation box.  
  
        The data flow extracts data from a file, looks up values in the CurrencyKey column in the DimCurrency table and the DateKey column in the DimDate table, and writes the data to the NewFactCurrencyRate table.
  
    To wrap the text in the annotation box, place the cursor where you want to start a new line and press **Enter**.  
  
    If you don't add text to the annotation box, the box disappears when you click outside it.  
  
## Go to next task
[Step 9: Test the Lesson 1 package](../integration-services/lesson-1-9-testing-the-lesson-1-tutorial-package.md)  
  
  
  
