---
title: "Evaluate Policies Dialog Box, Policy Selection Page | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dmf.runnow.f1"
ms.assetid: 20075fbe-0b48-42c8-b747-690f1aa23dcf
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Evaluate Policies Dialog Box, Policy Selection Page
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this dialog box to evaluate Policy-Based Management policies. By selecting the **Evaluation Results** page, you can apply policies to the items in a target set that do not comply with the policies.  
  
## Options  
 **Source**  
 Specifies the source of the policies. To change the source, click the Browse (**...**) button to open the **Select Source** dialog box.  
  
 **Files**  
 Type the path of a file that contains a Policy-Based Management policy, or use the Browse (**...**) button to select the file.  
  
 **Server**  
 Select to connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that contains the policy that you want.  
  
 **Policies: Policy**  
 Click to open the policy dialog box for the specified policy.  
  
 **Policies: Category**  
 The category of the policy. This box is read-only.  
  
 **Policies: Facet**  
 The facet implemented by the policy. This box is read-only.  
  
 **Evaluate**  
 Runs the policy in evaluation mode. This generates a compliance report for the target set but does not reconfigure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or enforce future compliance.  
  
## Possible Errors  
  
-   **No targets found**  
  
     The target set could be empty due to any of the following reasons:  
  
    -   There are no targets on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] of the type specified by the policy.  
  
    -   The server restriction might exclude the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that contains the target.  
  
    -   If the policy is on an object in a database (for example a table, view, or user) the database might not subscribe to the category of the policy.  
  
    -   The target-set filter might exclude all targets on this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    -   The target server type is different from the server type on which the policy is evaluated. For example, in the [!INCLUDE[ssDE](../../includes/ssde-md.md)], if you try to evaluate a policy that has been created for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you will receive an empty target set  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)   
 [Evaluate Policies Dialog Box, Evaluation Results Page](../../relational-databases/policy-based-management/evaluate-policies-dialog-box-evaluation-results-page.md)  
  
  
