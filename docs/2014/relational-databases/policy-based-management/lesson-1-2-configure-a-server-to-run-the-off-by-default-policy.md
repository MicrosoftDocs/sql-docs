---
title: "Configure a Server to Run the Off By Default Policy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 41c3022d-ab13-443e-ac64-ba1d64584f79
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure a Server to Run the Off By Default Policy
  Now you have a policy named Off By Default. In this task, you will check to see whether your server complies with the requirements of this policy.  
  
### To run the Off By Default policy  
  
1.  In Object Explorer, right-click your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], point to **Policies**, and then click **Evaluate**.  
  
2.  In the **Evaluate Policies** dialog box you can select policies from another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or from a file. For this step, leave **Source** set to your instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
3.  In the **Policies** section, select the **Off By Default** policy.  
  
4.  To see whether the server is in compliance with the policy, click **Evaluate**.  
  
5.  In the **Results** area, you will see a green circle with a check mark if the [!INCLUDE[ssDE](../../includes/ssde-md.md)] complies with the policy. You will see a red circle with an X if the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not comply with the policy.  
  
6.  In the **Target Details** area, you will see additional information in the **Message** column if an error occurs. In the **Message** column, click **View** to see a report that contains the results of the check for each facet property that was checked.  
  
7.  The policy description is displayed at the bottom of the page, and the **Additional help** section displays the hyperlink that you have configured for the policy. Click the message hyperlink to open the Web page that you specified when you created the policy.  
  
8.  Close the browser, and then close the **Results Detailed View** dialog box.  
  
9. If the server is out of compliance and you want to disable Database Mail, click **Apply** in the **Evaluation Results** page.  
  
10. Close both the **Results Detailed View** and the **Evaluate Policies** dialog boxes.  
  
## Next Lesson  
 [Lesson 2: Create and Apply a Naming Standards Policy](lesson-2-create-and-apply-a-naming-standards-policy.md)  
  
  
