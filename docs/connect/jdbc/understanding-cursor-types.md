---
title: "Understanding Cursor Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4f4d3db7-4f76-450d-ab63-141237a4f034
author: MightyPen
ms.author: genemi
manager: craigg
---
# Understanding Cursor Types
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  Operations in a relational database act on a complete set of rows. The set of rows returned by a SELECT statement consists of all the rows that satisfy the conditions in the WHERE clause of the statement. This complete set of rows returned by the statement is known as the result set. Applications cannot always work effectively with the entire result set as a unit. These applications need a mechanism to work with one row or a small block of rows at a time. Cursors are an extension to result sets that provide that mechanism.  
  
 Cursors extend result set processing by doing the following:  
  
-   Allowing positioning at specific rows of the result set.  
  
-   Retrieving one row or block of rows from the current position in the result set.  
  
-   Supporting data modifications to the row at the current position in the result set.  
  
-   Supporting different levels of visibility to changes made by other users to the database data that is presented in the result set.  
  
> [!NOTE]  
>  For a full description of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cursor types, see the "Cursor Types (Database Engine)" topic in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 The JDBC specification provides support for forward-only and scrollable cursors that are sensitive or insensitive to changes made by other jobs, and can be read-only or updatable. This functionality is provided by the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)][SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class.  
  
## Remarks  
 The JDBC driver supports the following cursor types:  
  
|Result Set<br /><br /> (Cursor) Type|SQL Server Cursor Type|Characteristics|select<br /><br /> Method|response<br /><br /> Buffering|Description|  
|------------------------------------|----------------------------|---------------------|-----------------------|----------------------------|-----------------|  
|TYPE_FORWARD_ONLY (CONCUR_READ_ONLY)|N/A|Forward-only, read-only|direct|full|The application has to make a single (forward) pass through the result set. This is the default behavior and behaves the same as a TYPE_SS_DIRECT_FORWARD_ONLY cursor. The driver reads the entire result set from the server into a memory during the statement execution time.|  
|TYPE_FORWARD_ONLY (CONCUR_READ_ONLY)|N/A|Forward-only, read-only|direct|adaptive|The application has to make a single (forward) pass through the result set. It behaves the same as a TYPE_SS_DIRECT_FORWARD_ONLY cursor. The driver reads rows from the server as the application requests them and thus minimizes the client-side memory usage.|  
|TYPE_FORWARD_ONLY (CONCUR_READ_ONLY)|Fast Forward|Forward-only, read-only|cursor|N/A|The application has to make a single (forward) pass through the result set by using a server cursor. It behaves the same as a TYPE_SS_SERVER_CURSOR_FORWARD_ONLY cursor.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_FORWARD_ONLY (CONCUR_UPDATABLE)|Dynamic (Forward-only)|Forward-only, updatable|N/A|N/A|The application has to make a single (forward) pass through the result set to update one or more rows.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.<br /><br /> By default, the fetch size is fixed when the application calls the [setFetchSize](../../connect/jdbc/reference/setfetchsize-method-sqlserverresultset.md) method of the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) object.<br /><br /> **Note:** The JDBC driver provides an adaptive buffering feature that allows you to retrieve statement execution results from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the application needs them, rather than all at once. For example, if the application should retrieve a large data that is too large to fit entirely in application memory, adaptive buffering allows the client application to retrieve such a value as a stream. The default behavior of the driver is "**adaptive**". However, in order to get the adaptive buffering for the forward-only updatable result sets, the application has to explicitly call the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object by providing a **String** value "**adaptive"**. For an example code, see [Updating Large Data Sample](../../connect/jdbc/updating-large-data-sample.md).|  
|TYPE_SCROLL_INSENSITIVE|Static|Scrollable, not updateable.<br /><br /> External row updates, inserts, and deletes are not visible.|N/A|N/A|The application requires a database snapshot. The result set is not updatable. Only CONCUR_READ_ONLY is supported.  All other concurrency types will cause an exception when used with this cursor type.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SCROLL_SENSITIVE<br /><br /> (CONCUR_READ_ONLY)|Keyset|Scrollable,  read-only. External row updates are visible, and deletes appear as missing data.<br /><br /> External row inserts are not visible.|N/A|N/A|The application has to see changed data for existing rows only.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SCROLL_SENSITIVE<br /><br /> (CONCUR_UPDATABLE, CONCUR_SS_SCROLL_LOCKS, CONCUR_SS_OPTIMISTIC_CC, CONCUR_SS_OPTIMISTIC_CCVAL)|Keyset|Scrollable, updatable.<br /><br /> External and internal row updates are visible, and deletes appear as missing data; inserts are not visible.|N/A|N/A|The application may change data in the existing rows by using the ResultSet object. The application must also be able to see the changes to rows made by others from outside the ResultSet object.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SS_DIRECT_FORWARD_ONLY|N/A|Forward-only, read-only|N/A|full or adaptive|Integer value = 2003. Provides a read-only client side cursor that is fully buffered. No server cursor is created.<br /><br /> Only CONCUR_READ_ONLY concurrency type is supported. All other concurrency types cause an exception when used with this cursor type.|  
|TYPE_SS_SERVER_CURSOR_FORWARD_ONLY|Fast Forward|Forward-only|N/A|N/A|Integer value = 2004. Fast, accesses all data using a server cursor. It is updatable when used with CONCUR_UPDATABLE concurrency type.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.<br /><br /> In order to get the adaptive buffering for this case, the application has to explicitly call the [setResponseBuffering](../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md) method of the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) object by providing a **String** value "**adaptive"**. For an example code, see [Updating Large Data Sample](../../connect/jdbc/updating-large-data-sample.md).|  
|TYPE_SS_SCROLL_STATIC|Static|Other users' updates are not reflected.|N/A|N/A|Integer value = 1004. Application requires a database snapshot. This is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific synonym for the JDBC TYPE_SCROLL_INSENSITIVE and has the same concurrency setting behavior.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SS_SCROLL_KEYSET<br /><br /> (CONCUR_READ_ONLY)|Keyset|Scrollable, read-only. External row updates are visible, and deletes appear as missing data.<br /><br /> External row inserts are not visible.|N/A|N/A|Integer value = 1005. Application has to see changed data for existing rows only. This is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific synonym for the JDBC TYPE_SCROLL_SENSITIVE and has the same concurrency setting behavior.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SS_SCROLL_KEYSET<br /><br /> (CONCUR_UPDATABLE, CONCUR_SS_SCROLL_LOCKS, CONCUR_SS_OPTIMISTIC_CC, CONCUR_SS_OPTIMISTIC_CCVAL)|Keyset|Scrollable, updatable.<br /><br /> External and internal row updates are visible, and deletes appear as missing data; inserts are not visible.|N/A|N/A|Integer value = 1005. Application has to change data or see changed data for existing rows. This is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific synonym for the JDBC TYPE_SCROLL_SENSITIVE and has the same concurrency setting behavior.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SS_SCROLL_DYNAMIC<br /><br /> (CONCUR_READ_ONLY)|Dynamic|Scrollable,  read-only.<br /><br /> External row updates and inserts are visible, and deletes appear as transient missing data in the current fetch buffer.|N/A|N/A|Integer value = 1006. Application must see changed data for existing rows, and see inserted and deleted rows during the lifetime of the cursor.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
|TYPE_SS_SCROLL_DYNAMIC<br /><br /> (CONCUR_UPDATABLE, CONCUR_SS_SCROLL_LOCKS, CONCUR_SS_OPTIMISTIC_CC, CONCUR_SS_OPTIMISTIC_CCVAL)|Dynamic|Scrollable, updatable.<br /><br /> External and internal row updates and inserts are visible, and deletes appear as transient missing data in the current fetch buffer.|N/A|N/A|Integer value = 1006. The application may change data for existing rows, or insert or delete rows by using the ResultSet object. The application must also be able to see changes, inserts, and deletes made by others from outside the ResultSet object.<br /><br /> Rows are retrieved from the server in blocks that are specified by the fetch size.|  
  
## Cursor Positioning  
 The TYPE_FORWARD_ONLY, TYPE_SS_DIRECT_FORWARD_ONLY, and TYPE_SS_SERVER_CURSOR_FORWARD_ONLY cursors support only the [next](../../connect/jdbc/reference/next-method-sqlserverresultset.md) positioning method.  
  
 The TYPE_SS_SCROLL_DYNAMIC cursor does not support the [absolute](../../connect/jdbc/reference/absolute-method-sqlserverresultset.md) and [getRow](../../connect/jdbc/reference/getrow-method-sqlserverresultset.md) methods. The absolute method can be approximated by a combination of calls to the [first](../../connect/jdbc/reference/first-method-sqlserverresultset.md) and [relative](../../connect/jdbc/reference/relative-method-sqlserverresultset.md) methods for dynamic cursors.  
  
 The getRow method is supported by TYPE_FORWARD_ONLY, TYPE_SS_DIRECT_FORWARD_ONLY, TYPE_SS_SERVER_CURSOR_FORWARD_ONLY, TYPE_SS_SCROLL_KEYSET, and TYPE_SS_SCROLL_STATIC cursors only. The getRow method with all forward-only cursor types returns the number of rows read so far through the cursor.  
  
> [!NOTE]  
>  When an application makes an unsupported cursor positioning call, or an unsupported call to the getRow method, an exception is thrown with the message, "The requested operation is not supported with this cursor type."  
  
 Only the TYPE_SS_SCROLL_KEYSET and the equivalent TYPE_SCROLL_SENSITIVE cursors expose deleted rows. If the cursor is positioned on a deleted row, column values are unavailable, and the [rowDeleted](../../connect/jdbc/reference/rowdeleted-method-sqlserverresultset.md) method returns "true". Calls to get\<Type> methods throw an exception with the message, "Cannot get value from a deleted row". Deleted rows cannot be updated. If you try to call an update\<Type> method on a deleted row, an exception is thrown with the message, "A deleted row cannot be updated". The TYPE_SS_SCROLL_DYNAMIC cursor has the same behavior until the cursor is moved out of the current fetch buffer.  
  
 Forward and dynamic cursors expose deleted rows in a similar way, but only while the cursors remain accessible in the fetch buffer. For forward cursors, this is fairly straightforward. For dynamic cursors it more complex when the fetch size is greater than 1. An application can move the cursor forward and backward within the window that is defined by the fetch buffer, but the deleted row will disappear when the original fetch buffer in which it was updated is left. If an application does not want to see transient deleted rows by using dynamic cursors, a fetch relative (0) should be used.  
  
 If the key values of a TYPE_SS_SCROLL_KEYSET or TYPE_SCROLL_SENSITIVE cursor row are updated with the cursor, the row retains its original position in the result set, regardless of whether the updated row meets the cursor's selection criteria. If the row was updated outside the cursor, a deleted row will appear at the row's original position, but the row will appear in the cursor only if another row with the new key values was present in the cursor, but has since been deleted.  
  
 For dynamic cursors, updated rows will retain their position within the fetch buffer until the window that is defined by the fetch buffer is left. Updated rows might subsequently reappear at different positions within the result set, or might disappear completely. Applications that have to avoid transient inconsistencies in the result set should use a fetch size of 1 (the default is 8 rows with CONCUR_SS_SCROLL_LOCKS concurrency and 128 rows with other concurrencies).  
  
## Cursor Conversion  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can sometimes choose to implement a cursor type other than the one requested, which is referred to as an implicit cursor conversion (or cursor degradation). For more information about implicit cursor conversion, see the "Using Implicit Cursor Conversions" topic in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 With [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], when you update the data through the ResultSet.TYPE_SCROLL_SENSITIVE and ResultSet.CONCUR_UPDATABLE result set, an exception is thrown with a message "The cursor is READ ONLY". This exception occurs because the [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] has done an implicit cursor conversion for that result set and did not return the updatable cursor that has been requested.  
  
 To work around this problem, you can do one of the following two solutions:  
  
-   Ensure that the underlying table has a primary key  
  
-   Use [SQLServerResultSet.TYPE_SS_SCROLL_DYNAMIC](../../connect/jdbc/reference/type-ss-scroll-dynamic-field-sqlserverresultset.md) instead of ResultSet.TYPE_SCROLL_SENSITIVE while creating a statement.  
  
## Cursor Updating  
 In-place updates are supported for cursors where the cursor type and concurrency support updates. If the cursor is not positioned on an updatable row in the result set (no get\<Type> method call succeeded), a call to an update\<Type> method will throw an exception with the message, "The result set has no current row." The JDBC specification states that an exception arises when an update method is called for a column of a cursor that is CONCUR_READ_ONLY. In situations where the row is not updatable, such as because of an optimistic concurrency conflict such as a competing update or deletion, the exception might not arise until [insertRow](../../connect/jdbc/reference/insertrow-method-sqlserverresultset.md), [updateRow](../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md), or [deleteRow](../../connect/jdbc/reference/deleterow-method-sqlserverresultset.md) is called.  
  
 After a call to update\<Type>, the affected column cannot be accessed by get\<Type> until updateRow or [cancelRowUpdates](../../connect/jdbc/reference/cancelrowupdates-method-sqlserverresultset.md) has been called. This avoids problems where a column is updated by using a different type from the type returned by the server, and subsequent getter calls could invoke client side type conversions that give inaccurate results. Calls to get\<Type> will throw an exception with the message, "Updated columns cannot be accessed until updateRow() or cancelRowUpdates() has been called."  
  
> [!NOTE]  
>  If the updateRow method is called when no columns have been updated, the JDBC driver will throw an exception with the message, "updateRow() called when no columns have been updated."  
  
 After [moveToInsertRow](../../connect/jdbc/reference/movetoinsertrow-method-sqlserverresultset.md) has been called, an exception will be thrown if any method other than get\<Type>, update\<Type>, insertRow, and cursor positioning methods (including [moveToCurrentRow](../../connect/jdbc/reference/movetocurrentrow-method-sqlserverresultset.md)) are called on the result set. The moveToInsertRow method effectively places the result set into insert mode, and cursor positioning methods terminate insert mode. Relative cursor positioning calls move the cursor relative to the position it was at before moveToInsertRow was called. After cursor positioning calls, the eventual destination cursor position becomes the new cursor position.  
  
 If the cursor positioning call made while in insert mode does not succeed, the cursor position after the failed call is the original cursor position before moveToInsetRow was called. If insertRow fails, the cursor remains on the insert row and the cursor remains in insert mode.  
  
 Columns in the insert row are initially in an uninitialized state. Calls to the update\<Type> method set the column state to initialized. A call to the get\<Type> method for an uninitialized column throws an exception. A call to the insertRow method returns all the columns in the insert row to an uninitialized state.  
  
 If any columns are uninitialized when the insertRow method is called, the default value for the column is inserted. If there is no default value but the column is nullable, then NULL is inserted. If there is no default value and the column is not nullable, the server will return an error and an exception will be thrown.  
  
> [!NOTE]  
>  Calls to the getRow method returns 0 when in insert mode.  
>   
>  The JDBC driver does not support positioned updates or deletes. According to the JDBC specification, the [setCursorName](../../connect/jdbc/reference/setcursorname-method-sqlserverstatement.md) method has no effect and the [getCursorName](../../connect/jdbc/reference/getcursorname-method-sqlserverresultset.md) method will throw an exception if called.  
>   
>  Read-only and static cursors are never updatable.  
>   
>  SQL Server restricts server cursors to a single result set. If a batch or stored procedure contains multiple statements, then a forward-only read-only client cursor must be used.  
  
## See Also  
 [Managing Result Sets with the JDBC Driver](../../connect/jdbc/managing-result-sets-with-the-jdbc-driver.md)  
  
  
