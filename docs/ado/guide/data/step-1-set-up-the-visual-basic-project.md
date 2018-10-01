---
title: "Step 1: Set Up the Visual Basic Project | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 77d3bfa5-fc9f-4a72-93b4-790c7d227988
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step 1: Set Up the Visual Basic Project
In this scenario, it is assumed that you have Microsoft Visual Basic 6.0, ADO 2.5 or later, and the Microsoft OLE DB Provider for Internet Publishing installed on your system. You will first create a new project, and then add some controls to the default form in the project.  
  
### To create an ADO project:  
  
1.  In Microsoft Visual Basic, create a new Standard EXE project.  
  
2.  From the Project menu, choose References.  
  
3.  Select "Microsoft ActiveX Data Objects 2.5 Library" and click OK.  
  
### To insert controls on the main form:  
  
1.  Add a ListBox control to Form1. Set its Name property to **lstMain**.  
  
2.  Add another ListBox control to Form1. Set its Name property to **lstDetails**.  
  
3.  Add a TextBox control to Form1. Set its Name property to **txtDetails**.  
  
## See Also  
 [Internet Publishing Scenario](../../../ado/guide/data/internet-publishing-scenario.md)   
 [Step 2: Initialize the Main List Box](../../../ado/guide/data/step-2-initialize-the-main-list-box.md)
