---
title: "Lesson 6: Run the RDL Schema Application (VB-C#) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a2cd2386-2df8-4b69-ab81-9ad1a31f6d27
author: craigg-msft
ms.author: douglasl
manager: craigg
---
# Lesson 6: Run the RDL Schema Application (VB-C#)
  [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] offers two methods to build and run a console application from the integrated development environment (IDE):  
  
-   Start (with Debugging)  
  
-   Start without Debugging  
  
### To build and run the SampleRDLSchema application  
  
1.  From the **Debug** menu, click **Start Without Debugging**. This ensures that the console window remains open after the program has finished executing.  
  
     The application prints the following output to the console:  
  
    ```  
    Loading Report Definition  
    Updating Report Definition  
    - Old Description: <Old Report Description>  
    - New Description: <New Report Description>  
    Publishing Report Definition  
    ```  
  
2.  Press any key to close the console.  
  
    > [!NOTE]  
    >  Any errors that occur are written to the console.  
  
3.  When the sample application is finished running an updated copy of the report will be saved to the report server.  
  
## See Also  
 [Updating Reports Using Classes Generated from the RDL Schema &#40;SSRS Tutorial&#41;](../../2014/tutorials/updating-reports-using-classes-generated-from-the-rdl-schema-ssrs-tutorial.md)   
 [Report Definition Language &#40;SSRS&#41;](../reporting-services/reports/report-definition-language-ssrs.md)  
  
  
