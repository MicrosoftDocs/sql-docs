---
title: "Step 8: Making the Lesson 1 Package Easier to Understand | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: e3751e53-77c7-47d0-8fe8-73ed1a53413a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Step 8: Making the Lesson 1 Package Easier to Understand
  Now that you have completed the configuration of the Lesson 1 package, it is a good idea to tidy up the package layout. If the shapes in the control and data flow layouts are random sizes, or if the shapes are not aligned or grouped, the functionality of package can be more difficult to understand.  
  
 [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools provides tools that make it easy and quick to format the package layout. The formatting features include the ability to make shapes the same size, align shapes, and manipulate the horizontal and vertical spacing between shapes.  
  
 Another way to improve the understanding of package functionality is to add annotations that describe package functionality.  
  
 In this task, you will use the formatting features in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools to improve the layout of the data flow and also add an annotation to the data flow.  
  
### To format the layout of the data flow  
  
1.  If the Lesson 1 package is not already open, double-click Lesson 1.dtsx in Solution Explorer.  
  
2.  Click the **Data Flow** tab.  
  
3.  Place the cursor to the top and to the right of the Extract Sample Currency transformation, click, and then drag the cursor across all the data flow components.  
  
4.  On the **Format** menu, point to **Make Same Size**, and then click **Both**.  
  
5.  With the data flow objects selected, on the **Format** menu, point to **Align**, and then click **Lefts**.  
  
### To add an annotation to the data flow  
  
1.  Right-click anywhere in the background of the data flow design surface and then click **Add Annotation**.  
  
2.  Type or paste the following text in the annotation box.  
  
     **The data flow extracts data from a file, looks up values in the CurrencyKey column in the DimCurrency table and the DateKey column in the DimDate table, and writes the data to the NewFactCurrencyRate table.**  
  
     To wrap the text in the annotation box, place the cursor where you want to start a new line and press the Enter key.  
  
     If you do not add text to the annotation box, it disappears when you click outside the box.  
  
## Next Steps  
 [Step 9: Testing the Lesson 1 Tutorial Package](../integration-services/lesson-1-9-testing-the-lesson-1-tutorial-package.md)  
  
  
