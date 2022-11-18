---
description: "Global Settings (Tester) (OracleToSQL)"
title: "Global Settings (Tester) (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 4acc0f2a-85ba-4c99-856a-89030f5c418e
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.oracle.globalsettingtester.f1"


---
# Global Settings (Tester) (OracleToSQL)
Use the Tester page of the **Global Settings** dialog box to specify settings for SSMA Tester.  
  
To access the Tester settings, on the **Tools** menu, select **Global Settings**, and click **Tester** at the bottom of the left pane.  
  
## Options  
**Testable object analysis**  
This setting specifies whether to perform the analysis of the Testable objects. Select **Yes** if you want SSMA Tester to analyze and automatically check the dependent objects. Default option set is **Yes**.  
  
The following options are available for this setting:  
  
1.  Yes  
  
2.  No  
  
**Auxiliary tables saving mode**  
This setting specifies how to save the internal auxiliary tables created during the test case execution. Following options can be set for this particular setting:  
  
1.  Always Delete  
  
2.  Always Save  
  
3.  Save if Table Comparison Failed  
  
4.  Ask User if Table Comparison Failed  
  
The default option set is: **Always Delete**.  
  
**Perform data rollback**  
This setting specifies whether to perform a rollback operation after each test case is run. Default option set is **No**.  
  
The following options are available for this setting:  
  
1.  Yes  
  
2.  No  
  
**Stop test execution after first failure**  
This setting specifies whether to stop the current running test case, if an error has occurred while execution. Default option set is **Yes**.  
  
The following options are available for this setting:  
  
1.  Yes  
  
2.  No  
  
## See Also  
[Finishing Test Case Preparation &#40;OracleToSQL&#41;](../../ssma/oracle/finishing-test-case-preparation-oracletosql.md)  
  
