---
title: "SQLServerResultSet Members"
description: "SQLServerResultSet Members"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerResultSet Members
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  The following tables list the members that are exposed by the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) class.  
  
## Constructors  
 None.  
  
## Fields  
  
|Name|Description|  
|----------|-----------------|  
|[CONCUR_SS_OPTIMISTIC_CC](../../../connect/jdbc/reference/concur-ss-optimistic-cc-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] read/write optimistic concurrency type with no row locks.|  
|[CONCUR_SS_OPTIMISTIC_CCVAL](../../../connect/jdbc/reference/concur-ss-optimistic-ccval-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] read/write optimistic concurrency type with no row locks.|  
|[CONCUR_SS_SCROLL_LOCKS](../../../connect/jdbc/reference/concur-ss-scroll-locks-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] read/write optimistic concurrency type with row locks.|  
|[TYPE_SS_DIRECT_FORWARD_ONLY](../../../connect/jdbc/reference/type-ss-direct-forward-only-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] fast forward-only, read-only cursor type.|  
|[TYPE_SS_SCROLL_DYNAMIC](../../../connect/jdbc/reference/type-ss-scroll-dynamic-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] dynamic cursor type.|  
|[TYPE_SS_SCROLL_KEYSET](../../../connect/jdbc/reference/type-ss-scroll-keyset-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] keyset cursor type.|  
|[TYPE_SS_SCROLL_STATIC](../../../connect/jdbc/reference/type-ss-scroll-static-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] static cursor type.|  
|[TYPE_SS_SERVER_CURSOR_FORWARD_ONLY](../../../connect/jdbc/reference/type-ss-server-cursor-forward-only-field-sqlserverresultset.md)|Used to specify a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] fast forward-only, read-only cursor type.|  
  
## Inherited Fields  
  
|Class inherited from:|Description|  
|---------------------------|-----------------|  
|java.sql.ResultSet|CLOSE_CURSORS_AT_COMMIT, CONCUR_READ_ONLY, CONCUR_UPDATABLE, FETCH_FORWARD, FETCH_REVERSE, FETCH_UNKNOWN, HOLD_CURSORS_OVER_COMMIT, TYPE_FORWARD_ONLY, TYPE_SCROLL_INSENSITIVE, TYPE_SCROLL_SENSITIVE|  
  
## Methods  
  
|Name|Description|  
|----------|-----------------|  
|[absolute](../../../connect/jdbc/reference/absolute-method-sqlserverresultset.md)|Moves the cursor to the specified row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[afterLast](../../../connect/jdbc/reference/afterlast-method-sqlserverresultset.md)|Moves the cursor to after the last row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[beforeFirst](../../../connect/jdbc/reference/beforefirst-method-sqlserverresultset.md)|Moves the cursor to before the first row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[cancelRowUpdates](../../../connect/jdbc/reference/cancelrowupdates-method-sqlserverresultset.md)|Cancels the updates made to the current row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[clearWarnings](../../../connect/jdbc/reference/clearwarnings-method-sqlserverresultset.md)|Clears all warnings reported on this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[close](../../../connect/jdbc/reference/close-method-sqlserverresultset.md)|Releases this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object's database and JDBC resources immediately instead of waiting for this to happen when it is automatically closed.|  
|[deleteRow](../../../connect/jdbc/reference/deleterow-method-sqlserverresultset.md)|Deletes the current row from this[SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object and from the underlying database.|  
|[finalize](../../../connect/jdbc/reference/finalize-method-sqlserverresultset.md)|Explicitly closes this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[findColumn](../../../connect/jdbc/reference/findcolumn-method-sqlserverresultset.md)|Retrieves the index of the first matching column for the specified column name in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[first](../../../connect/jdbc/reference/first-method-sqlserverresultset.md)|Moves the cursor to the first row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getArray](../../../connect/jdbc/reference/getarray-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as an Array object.|  
|[getAsciiStream](../../../connect/jdbc/reference/getasciistream-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a stream of ASCII characters.|  
|[getBigDecimal](../../../connect/jdbc/reference/getbigdecimal-method-sqlserverresultset.md)|Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.math.BigDecimal.|  
|[getBinaryStream](../../../connect/jdbc/reference/getbinarystream-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a binary stream of uninterpreted bytes.|  
|[getBlob](../../../connect/jdbc/reference/getblob-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Blob object in the Java programming language.|  
|[getBoolean](../../../connect/jdbc/reference/getboolean-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **boolean** in the Java programming language.|  
|[getByte](../../../connect/jdbc/reference/getbyte-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **byte** in the Java programming language.|  
|[getBytes](../../../connect/jdbc/reference/getbytes-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **byte** array in the Java programming language.|  
|[getCharacterStream](../../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.io.Reader object.|  
|[getClob](../../../connect/jdbc/reference/getclob-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Clob object in the Java programming language.|  
|[getConcurrency](../../../connect/jdbc/reference/getconcurrency-method-sqlserverresultset.md)|Retrieves the concurrency mode of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getCursorName](../../../connect/jdbc/reference/getcursorname-method-sqlserverresultset.md)|Retrieves the name of the SQL cursor used by this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getDate](../../../connect/jdbc/reference/getdate-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.sql.Date object in the Java programming language.|  
|[getDateTimeOffset](../../../connect/jdbc/reference/getdatetimeoffset-sqlserverresultset.md)|Retrieves the value of the specified column as a[DateTimeOffset Class](../../../connect/jdbc/reference/datetimeoffset-class.md) object.|  
|[getDouble](../../../connect/jdbc/reference/getdouble-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **double** in the Java programming language.|  
|[getFetchDirection](../../../connect/jdbc/reference/getfetchdirection-method-sqlserverresultset.md)|Retrieves the fetch direction for this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getFetchSize](../../../connect/jdbc/reference/getfetchsize-method-sqlserverresultset.md)|Retrieves the fetch size for this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getFloat](../../../connect/jdbc/reference/getfloat-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **float** in the Java programming language.|  
|[getHoldability](../../../connect/jdbc/reference/getholdability-method-sqlserverresultset.md)|Retrieves the holdability of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getInt](../../../connect/jdbc/reference/getint-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as an **int** in the Java programming language.|  
|[getLong](../../../connect/jdbc/reference/getlong-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **long** in the Java programming language.|  
|[getMetaData](../../../connect/jdbc/reference/getmetadata-method-sqlserverresultset.md)|Retrieves the number, types, and properties of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object's columns.|  
|[getNCharacterStream](../../../connect/jdbc/reference/getncharacterstream-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Reader object.|  
|[getNClob](../../../connect/jdbc/reference/getnclob-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of the  [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md)object as an **NClob** object in the Java programming language.|  
|[getNString](../../../connect/jdbc/reference/getnstring-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a String in the Java programming language.|  
|[getObject](../../../connect/jdbc/reference/getobject-method-sqlserverresultset.md)|Gets the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as an object in the Java programming language.|  
|[getRef](../../../connect/jdbc/reference/getref-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Ref object in the Java programming language.|  
|[getRow](../../../connect/jdbc/reference/getrow-method-sqlserverresultset.md)|Retrieves the current row number.|  
|[getShort](../../../connect/jdbc/reference/getshort-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **short** in the Java programming language.|  
|[getStatement](../../../connect/jdbc/reference/getstatement-method-sqlserverresultset.md)|Retrieves the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that produced this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getString](../../../connect/jdbc/reference/getstring-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **String** in the Java programming language.|  
|[getSQLXML](../../../connect/jdbc/reference/getsqlxml-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **SQLXML** object.|  
|[getTime](../../../connect/jdbc/reference/gettime-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.sql.Time object in the Java programming language.|  
|[getTimestamp](../../../connect/jdbc/reference/gettimestamp-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.sql.Timestamp object in the Java programming language.|  
|[getType](../../../connect/jdbc/reference/gettype-method-sqlserverresultset.md)|Retrieves the cursor type of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getUnicodeStream](../../../connect/jdbc/reference/getunicodestream-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a stream of Unicode characters.|  
|[getURL](../../../connect/jdbc/reference/geturl-method-sqlserverresultset.md)|Retrieves the value of the designated column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a URL object.|  
|[getWarnings](../../../connect/jdbc/reference/getwarnings-method-sqlserverresultset.md)|Retrieves the first warning reported by calls on this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[insertRow](../../../connect/jdbc/reference/insertrow-method-sqlserverresultset.md)|Inserts the contents of the insert row into this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object and into the database.|  
|[isAfterLast](../../../connect/jdbc/reference/isafterlast-method-sqlserverresultset.md)|Retrieves whether the cursor is after the last row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[isBeforeFirst](../../../connect/jdbc/reference/isbeforefirst-method-sqlserverresultset.md)|Retrieves whether the cursor is before the first row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[isClosed](../../../connect/jdbc/reference/isclosed-method-sqlserverresultset.md)|Indicates whether this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object has been closed.|  
|[isFirst](../../../connect/jdbc/reference/isfirst-method-sqlserverresultset.md)|Retrieves whether the cursor is on the first row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[isLast](../../../connect/jdbc/reference/islast-method-sqlserverresultset.md)|Retrieves whether the cursor is on the last row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[last](../../../connect/jdbc/reference/last-method-sqlserverresultset.md)|Moves the cursor to the last row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[moveToCurrentRow](../../../connect/jdbc/reference/movetocurrentrow-method-sqlserverresultset.md)|Moves the cursor to the remembered cursor position, usually the current row.|  
|[moveToInsertRow](../../../connect/jdbc/reference/movetoinsertrow-method-sqlserverresultset.md)|Moves the cursor to the insert row.|  
|[next](../../../connect/jdbc/reference/next-method-sqlserverresultset.md)|Moves the cursor down one row from its current position.|  
|[previous](../../../connect/jdbc/reference/previous-method-sqlserverresultset.md)|Moves the cursor to the previous row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[refreshRow](../../../connect/jdbc/reference/refreshrow-method-sqlserverresultset.md)|Refreshes the current row with its most recent value in the database.|  
|[relative](../../../connect/jdbc/reference/relative-method-sqlserverresultset.md)|Moves the cursor the given amount of rows, relative to the current row, in either a positive or a negative direction.|  
|[rowDeleted](../../../connect/jdbc/reference/rowdeleted-method-sqlserverresultset.md)|Retrieves whether a row has been deleted.|  
|[rowInserted](../../../connect/jdbc/reference/rowinserted-method-sqlserverresultset.md)|Retrieves whether the current row has had an insertion.|  
|[rowUpdated](../../../connect/jdbc/reference/rowupdated-method-sqlserverresultset.md)|Retrieves whether the current row has been updated.|  
|[setFetchDirection](../../../connect/jdbc/reference/setfetchdirection-method-sqlserverresultset.md)|Gives a hint as to the direction in which the rows in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object will be processed.|  
|[setFetchSize](../../../connect/jdbc/reference/setfetchsize-method-sqlserverresultset.md)|Gives the JDBC driver a hint as to the number of rows that should be fetched from the database when more rows are needed for this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[updateArray](../../../connect/jdbc/reference/updatearray-method-sqlserverresultset.md)|Updates the designated column with an Array object.|  
|[updateAsciiStream](../../../connect/jdbc/reference/updateasciistream-method-sqlserverresultset.md)|Updates the designated column with an ASCII stream value.|  
|[updateBigDecimal](../../../connect/jdbc/reference/updatebigdecimal-method-sqlserverresultset.md)|Updates the designated column with a BigDecimal object.|  
|[updateBinaryStream](../../../connect/jdbc/reference/updatebinarystream-method-sqlserverresultset.md)|Updates the designated column with a binary stream value.|  
|[updateBlob](../../../connect/jdbc/reference/updateblob-method-sqlserverresultset.md)|Updates the designated column with a java.sql.Blob value.|  
|[updateBoolean](../../../connect/jdbc/reference/updateboolean-method-sqlserverresultset.md)|Updates the designated column with a **boolean** value.|  
|[updateByte](../../../connect/jdbc/reference/updatebyte-method-sqlserverresultset.md)|Updates the designated column with a **byte** value.|  
|[updateBytes](../../../connect/jdbc/reference/updatebytes-method-sqlserverresultset.md)|Updates the designated column with an array of **byte** values.|  
|[updateCharacterStream](../../../connect/jdbc/reference/updatecharacterstream-method-sqlserverresultset.md)|Updates the designated column with a character stream value.|  
|[updateClob](../../../connect/jdbc/reference/updateclob-method-sqlserverresultset.md)|Updates the designated column with a java.sql.Clob value.|  
|[updateDate](../../../connect/jdbc/reference/updatedate-method-sqlserverresultset.md)|Updates the designated column with a date value.|  
|[updateDateTimeOffset](../../../connect/jdbc/reference/updatedatetimeoffset-sqlserverresultset.md)|Updates a [DateTimeOffset Class](../../../connect/jdbc/reference/datetimeoffset-class.md) column.|  
|[updateDouble](../../../connect/jdbc/reference/updatedouble-method-sqlserverresultset.md)|Updates the designated column with a **double** value.|  
|[updateFloat](../../../connect/jdbc/reference/updatefloat-method-sqlserverresultset.md)|Updates the designated column with a **float** value.|  
|[updateInt](../../../connect/jdbc/reference/updateint-method-sqlserverresultset.md)|Updates the designated column with an **int** value.|  
|[updateLong](../../../connect/jdbc/reference/updatelong-method-sqlserverresultset.md)|Updates the designated column with a **long** value.|  
|[updateNCharacterStream](../../../connect/jdbc/reference/updatencharacterstream-method-sqlserverresultset.md)|Updates the designated column with a character stream value.|  
|[updateNClob](../../../connect/jdbc/reference/updatenclob-method-sqlserverresultset.md)|Updates the designated column with the specified object value.|  
|[updateNString](../../../connect/jdbc/reference/updatenstring-method-sqlserverresultset.md)|Updates the designated column with a **String** value.|  
|[updateNull](../../../connect/jdbc/reference/updatenull-method-sqlserverresultset.md)|Updates the designated column with a null value.|  
|[updateObject](../../../connect/jdbc/reference/updateobject-method-sqlserverresultset.md)|Updates the designated column with an **Object** value.|  
|[updateRef](../../../connect/jdbc/reference/updateref-method-sqlserverresultset.md)|Updates the designated column with a java.sql.Ref value.|  
|[updateRow](../../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md)|Updates the underlying database with the new contents of the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[updateShort](../../../connect/jdbc/reference/updateshort-method-sqlserverresultset.md)|Updates the designated column with a **short** value.|  
|[updateString](../../../connect/jdbc/reference/updatestring-method-sqlserverresultset.md)|Updates the designated column with a **String** value.|  
|[updateSQLXML](../../../connect/jdbc/reference/updatesqlxml-method-sqlserverresultset.md)|Updates the designated column with a **SQLXML** value.|  
|[updateTime](../../../connect/jdbc/reference/updatetime-method-sqlserverresultset.md)|Updates the designated column with a time value.|  
|[updateTimestamp](../../../connect/jdbc/reference/updatetimestamp-method-sqlserverresultset.md)|Updates the designated column with a timestamp value.|  
|[wasNull](../../../connect/jdbc/reference/wasnull-method-sqlserverresultset.md)|Verifies whether the last value read was a null value.|  
  
## Inherited Methods  
  
|Class inherited from:|Methods|  
|---------------------------|-------------|  
|java.lang.Object|clone, equals, getClass, hashCode, notify, notifyAll, toString, wait|  
|java.sql.Wrapper|isWrapperFor, unwrap|  
  
## See Also  
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
