---
title: "Static Data Masking | Microsoft Docs"
ms.date: "11/05/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
ms.assetid: a62f4ff9-2953-42ca-b7d8-1f8f527c4d66
author: esgranet
ms.author: esgranet
manager: ajayj
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Static Data Masking
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

![Static data masking](../../relational-databases/security/media/sql-static-data-masking/static_data_masking_intro_image.PNG)

Static Data Masking is a privacy tool that helps users to anonymize/de-identify their databases by applying a mask on columns with sensitive information. Masking is the process of replacing original data (pre-masking) with new data (post-masking). The algorithm in charge of the replacement is called masking function.  For example, below is a masking function that scrambles the first 5 digits of US social security numbers.


## See Also  
 [Dynamic Data Masking](../../relational-databases/security/dynamic-data-masking.md)   
 [Get started with SQL Database Static Data Masking (Azure Preview portal)](http://azure.microsoft.com/documentation/articles/sql-database-static-data-masking-get-started/)  
