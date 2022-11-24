---
description: "Data Length, Buffer Length, and Truncation"
title: "Data Length, Buffer Length, and Truncation | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data buffers [ODBC], length"
  - "data length [ODBC]"
  - "truncating data [ODBC]"
  - "length of data buffers [ODBC]"
  - "buffers [ODBC], length"
ms.assetid: 2825c6e7-b9ff-42fe-84fc-7fb39728ac5d
author: David-Engel
ms.author: v-davidengel
---
# Data Length, Buffer Length, and Truncation
The *data length* is the byte length of the data as it would be stored in the application's data buffer, not as it is stored in the data source. This distinction is important because the data is often stored in different types in the data buffer than in the data source. So for data being sent to the data source, this is the byte length of the data before conversion to the data source's type. For data being retrieved from the data source, this is the byte length of the data after conversion to the data buffer's type and before any truncation is done.  
  
 For fixed-length data, such as an integer or a date structure, the byte length of the data is always the size of the data type. In general, applications allocate a data buffer that is the size of the data type. If the application allocates a smaller buffer, the consequences are undefined because the driver assumes the data buffer is the size of the data type and does not truncate the data to fit into a smaller buffer. If the application allocates a larger buffer, the extra space is never used.  
  
 For variable-length data, such as character or binary data, it is important to recognize that the byte length of the data is separate from and often different than the byte length of the buffer. The relation of these two lengths is described in the [Buffers](../../../odbc/reference/develop-app/buffers.md) section. If the byte length of the data is greater than the byte length of the buffer, the driver truncates fetched data to the byte length of the buffer and returns SQL_SUCCESS_WITH_INFO with SQLSTATE 01004 (Data truncated). However, the returned byte length is the length of the untruncated data.  
  
 For example, suppose an application allocates 50 bytes for a binary data buffer. If the driver has 10 bytes of binary data to return, it returns those 10 bytes in the buffer. The byte length of the data is 10, and the byte length of the buffer is 50. If the driver has 60 bytes of binary data to return, it truncates the data to 50 bytes, returns those bytes in the buffer, and returns SQL_SUCCESS_WITH_INFO. The byte length of the data is 60 (the length before truncation), and the byte length of the buffer is still 50.  
  
 A diagnostic record is created for each column that is truncated. Because it takes time for the driver to create these records and for the application to process them, truncation can degrade performance. Usually, an application can avoid this problem by allocating large enough buffers, although this might not be possible when working with long data. When data truncation occurs, the application can sometimes allocate a larger buffer and refetch the data; this is not true in all cases. If truncation occurs while getting data with calls to **SQLGetData**, the application need not call **SQLGetData** for data that has already been returned; for more information, see [Getting Long Data](../../../odbc/reference/develop-app/getting-long-data.md).
