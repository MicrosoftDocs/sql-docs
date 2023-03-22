---
title: "Lesson 7: Add Drillthrough Action on Parent Report"
description: Learn how to add a drillthrough action on the parent report after you add a ReportViewer control to the website application.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/18/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 7: Add Drillthrough Action on Parent Report
After you add a ReportViewer control to the website application, your next step is to add a drillthrough action on the parent report.  
  
### To add drillthrough action on the parent report  
  
1.  Go to the parent report.  
  
2.  Select the textbox that holds the value of **Name**.  
  
3.  Right-click the textbox, and then select **Text Box Properties**.  
  
4.  Go to the **Action** tab, and then select the **Go to report** option.  
  
5.  Enter the name of the child report in the **Specify a report** section.  
  
    > [!NOTE]
    > Do not include the file extension for the report name.  
  
6.  Select **Add** under **Use these parameters to run the report** section.  
  
7.  Type **productid** in the **name** box, and then select **ProductID** in the **Value** drop-down list.  
  
8.  Select **Ok** to finish.  
  
## Next Task  
You have successfully added a drillthrough action on the parent report. Next, you will create a data filter for the data table that you defined for the child report. See [Lesson 8: Create a Data Filter](../reporting-services/lesson-8-create-a-data-filter.md).  
  
  
  

