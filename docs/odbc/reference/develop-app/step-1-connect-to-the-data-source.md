---
title: "Step 1: Connect to the Data Source"
description: "Step 1: Connect to the Data Source"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "application process [ODBC], connecting to data source"
  - "data sources [ODBC], connections"
  - "connecting to data source [ODBC], steps"
---
# Step 1: Connect to the Data Source
The first step in any application is to connect to the data source. This phase, including the functions it requires, is shown in the following illustration.  
  
 ![Connecting to the data source in an ODBC app](../../../odbc/reference/develop-app/media/pr11.gif "pr11")  
  
 The first step in connecting to the data source is to load the Driver Manager and allocate the environment handle with **SQLAllocHandle**. For more information, see [Allocating the Environment Handle](../../../odbc/reference/develop-app/allocating-the-environment-handle.md).  
  
 The application then registers the version of ODBC to which it conforms by calling **SQLSetEnvAttr** with the SQL_ATTR_APP_ODBC_VER environment attribute. For more information, see [Declaring the Application's ODBC Version](../../../odbc/reference/develop-app/declaring-the-application-s-odbc-version.md) and [Backward Compatibility and Standards Compliance](../../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md).  
  
 Next, the application allocates a connection handle with **SQLAllocHandle** and connects to the data source with **SQLConnect**, **SQLDriverConnect**, or **SQLBrowseConnect**. For more information, see [Allocating a Connection Handle](../../../odbc/reference/develop-app/allocating-a-connection-handle-odbc.md) and [Establishing a Connection](../../../odbc/reference/develop-app/establishing-a-connection.md).  
  
 The application then sets any connection attributes, such as whether to manually commit transactions. For more information, see [Connection Attributes](../../../odbc/reference/develop-app/connection-attributes.md).
