---
description: "Results Detailed View Dialog Box"
title: "Results Detailed View Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.results.f1"
  - "sql13.swb.dmf.policy.resultdetails.f1"
ms.assetid: 366f0ff8-722a-40a9-934f-854147e4933d
author: VanMSFT
ms.author: vanto
---
# Results Detailed View Dialog Box
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This dialog box shows the results of policy evaluation after you have run a policy by using the **Evaluate Policies** dialog box and clicked **Evaluate**. This dialog box is read-only, and helps you understand which part of a property expression might be failing.  
  
## Options  
 **AndOr**  
 When more than one property expression is present, indicates whether the property expressions are cumulative or alternative.  
  
 **Result**  
 Icon that represents the success or failure of the property expression.  
  
 **Field**  
 The property of the facet that is being modeled.  
  
 **Operator**  
 The operator for the expression, for example **=** or **Like**.  
  
 **Expected Value**  
 The value for the field that will cause the property expression to be successful.  
  
 **Actual Value**  
 The value for the field that was detected by the policy.  
  
 **Policy description**  
 The description of the policy.  
  
 **Additional help**  
 Click the hyperlink to open a Web page that is related to this policy. The Additional Help hyperlink is configured when the policy is created and might be blank or unavailable.  
  
## See Also  
 [Policy Management Node &#40;Object Explorer&#41;](../../relational-databases/policy-based-management/policy-management-node-object-explorer.md)   
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
