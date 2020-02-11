---
title: "Import Policies Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dmf.importpolicy.f1"
ms.assetid: 78ab5f6e-2f13-4788-937e-8892ef4e2345
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Import Policies Dialog Box
  Use this dialog box to import one or more policies (and their referenced condition) that are saved as XML files, into the current [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance.  
  
## Options  
 **Files to import**  
 To import a policy from an XML file, type the path and name of the file or use the Browse (**...**) button.  
  
 **Replace duplicates with items imported**  
 Select to overwrite any existing policy or condition of the same name if it already exists on this [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance. A condition with a dependent policy cannot be overwritten unless the dependent policy is also being overwritten. If this option is not selected, an existing condition that is using the same condition expression will not cause an error.  
  
 **Policy state**  
 Select the state that you want for the imported policy:  
  
-   **Preserve policy state on import**  
  
-   **Enable all policies on import**  
  
-   **Disable all policies on import**  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)   
 [Import a Policy-Based Management Policy](import-a-policy-based-management-policy.md)   
 [Export a Policy-Based Management Policy](export-a-policy-based-management-policy.md)  
  
  
