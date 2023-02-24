---
description: "Appendix G: Driver Guidelines for Backward Compatibility"
title: "Appendix G: Driver Guidelines for Backward Compatibility | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "ODBC drivers [ODBC], backward compatibility"
  - "backward compatibility [ODBC], drivers"
  - "compatibility [ODBC], drivers"
ms.assetid: 911cd335-f2c0-4d03-9739-1078308a678a
author: David-Engel
ms.author: v-davidengel
---
# Appendix G: Driver Guidelines for Backward Compatibility
This appendix provides information for driver writers working on ODBC 3.*x* drivers that need to support ODBC 2.*x* applications. For more information about backward compatibility, see [Backward Compatibility and Standards Compliance](../../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md).  
  
 This section contains the following topics.  
  
-   [Block Cursors, Scrollable Cursors, and Backward Compatibility for ODBC 3.x Drivers](../../../odbc/reference/appendixes/block-cursors-scrollable-cursors-and-backward-compatibility.md) - New features are features that exist in ODBC 3.*x* and not in ODBC 2.*x*. ODBC 3.*x* drivers generally do not have to worry about backward compatibility with new features because ODBC 2.*x* applications never use them. The sole exceptions to this are features related to **SQLFetch**, **SQLFetchScroll**, **SQLSetPos**, and **SQLExtendedFetch**; for more information, see , later in this appendix.  
  
-   [Mapping Deprecated Functions](../../../odbc/reference/appendixes/mapping-deprecated-functions.md) - Duplicated features are features that are implemented differently in ODBC 3.*x* and ODBC 2.*x*. ODBC 3.*x* drivers do not have to worry about backward compatibility with duplicated features because the Driver Manager always maps ODBC 2.*x* features to ODBC 3.*x* features when calling an ODBC 3.*x* driver. Thus, an ODBC 3.*x* driver sees only ODBC 3.*x* features. For more information about these mappings, see , later in this appendix.  
  
-   [Behavioral Changes and ODBC 3.x Drivers](../../../odbc/reference/appendixes/behavioral-changes-and-odbc-3-x-drivers.md) - Behavior changes are features that are handled differently in ODBC 3.*x* and ODBC 2.*x*. ODBC 3.*x* drivers have to worry about behavior changes and act in response to the SQL_ATTR_ODBC_VERSION environment attribute set by the application.
