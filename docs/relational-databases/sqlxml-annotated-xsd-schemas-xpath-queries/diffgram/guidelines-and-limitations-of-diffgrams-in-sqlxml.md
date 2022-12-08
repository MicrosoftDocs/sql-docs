---
title: "Guidelines and Limitations of DiffGrams in SQLXML"
description: Guidelines and Limitations of DiffGrams in SQLXML
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/16/2017"
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "DiffGrams [SQLXML], about DiffGrams"
ms.assetid: cf8689c4-2a63-4d05-b202-21b5ff187d7f
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Guidelines and Limitations of DiffGrams in SQLXML
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  Remember the following when using DiffGrams with SQLXML 4.0:  
  
-   Binary large object (BLOB) types like **text/ntext** and images should not be used in the **\<diffgr:before>** block in when working with DiffGrams, because this will include them for use in concurrency control. This can cause problems with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] because of the limitations on comparison for BLOB types. For example, the LIKE keyword is used in the WHERE clause for comparing between columns of the **text** data type; however, comparisons will fail in the case of BLOB types where the size of the data is greater than 8K.  
  
-   Special characters in **ntext** data can cause problems with SQLXML 4.0 because of the limitations on comparison for BLOB types. For example, the use of "[Serializable]" in the **\<diffgr:before>** block of a DiffGram when used in concurrency checking of a column of **ntext** type will fail with the following SQLOLEDB error description:  
  
    ```  
    Empty update, no updatable rows found   Transaction aborted  
    ```  
  
  
