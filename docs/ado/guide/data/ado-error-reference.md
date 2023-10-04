---
title: "ADO Error Reference"
description: "ADO Error Reference"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "errors [ADO], number reference"
  - "errors [ADO], ErrorValueEnum"
  - "ErrorValueEnum enumeration [ADO]"
---
# ADO Errors
The **ErrorValueEnum** constant describes the ADO error values. For a complete listing of these enumerated constants, including values, see [Appendix B: ADO Errors](../appendixes/appendix-b-ado-errors.md). This section will examine some of the more interesting errors and explain some specific situations that can raise them, or solutions to fix the problem. Both the **ErrorValueEnum** constant and the short positive decimal number are listed.

|Number|ErrorValueEnum constant|Description/Possible causes|
|------------|-----------------------------|----------------------------------|
|**3000**|**adErrProviderFailed**|Provider failed to perform the requested operation.|
|**3001**|**adErrInvalidArgument**|Arguments are of the wrong type, are out of acceptable range, or are in conflict with one another. This error is often caused by a typographical error in an SQL SELECT statement. For example, a misspelled field name or table name can generate this error. This error can also occur when a field or table named in a SELECT statement does not exist in the data store.|
|**3002**|**adErrOpeningFile**|File could not be opened. A misspelled file name was specified, or a file has been moved, renamed, or deleted. Over a network, the drive might be temporarily unavailable or network traffic might be preventing a connection.|
|**3003**|**adErrReadFile**|File could not be read. The name of the file is specified incorrectly, the file might have been moved or deleted, or the file might have become corrupted.|
|**3004**|**adErrWriteFile**|Write to file failed. You might have closed a file and then tried to write to it, or the file might be corrupted. If the file is located on a network drive, transient network conditions might prevent writing to a network drive.|
|**3021**|**adErrNoCurrentRecord**|Either **BOF** or **EOF** is True, or the current record has been deleted. Requested operation requires a current record.<br /><br /> An attempt was made to update records by using **Find** or **Seek** to move the record pointer to the desired record. If the record is not found, **EOF** will be True. This error can also occur after a failed **AddNew** or **Delete** because there is no current record when these methods fail.|
|**3219**|**adErrIllegalOperation**|Operation is not allowed in this context.|
|**3220**|**adErrCantChangeProvider**|Supplied provider is different from the one already in use.|
|**3246**|**adErrInTransaction**|**Connection** object cannot be explicitly closed while in a transaction. A **Recordset** or **Connection** object that is currently participating in a transaction cannot be closed. Call either **RollbackTrans** or **CommitTrans** before closing the object.|
|**3251**|**adErrFeatureNotAvailable**|The object or provider is not capable of performing the requested operation. Some operations depend on a particular provider version.|
|**3265**|**adErrItemNotFound**|Item cannot be found in the collection corresponding to the requested name or ordinal. An incorrect field or table name has been specified.|
|**3367**|**adErrObjectInCollection**|Object is already in collection. Cannot append. An object cannot be added to the same collection twice.|
|**3420**|**adErrObjectNotSet**|Object is no longer valid.|
|**3421**|**adErrDataConversion**|Application uses a value of the wrong type for the current operation. You might have supplied a string to an operation that expects a stream, for example.|
|**3704**|**adErrObjectClosed**|Operation is not allowed when the object is closed. The **Connection** or **Recordset** has been closed. For example, some other routine might have closed a global object. You can prevent this error by checking the **State** property before you attempt an operation.|
|**3705**|**adErrObjectOpen**|Operation is not allowed when the object is open. An object that is open cannot be opened. Fields cannot be appended to an open **Recordset**.|
|**3706**|**adErrProviderNotFound**|Provider cannot be found. It may not be properly installed.<br /><br /> The name of the provider might be incorrectly specified, the specified provider might not be installed on the computer where your code is being executed, or the installation might have become corrupted.|
|**3707**|**adErrBoundToCommand**|The **ActiveConnection** property of a **Recordset** object, which has a **Command** object as its source, cannot be changed. The application attempted to assign a new **Connection** object to a **Recordset** that has a **Command** object as its source.|
|**3708**|**adErrInvalidParamInfo**|**Parameter** object is improperly defined. Inconsistent or incomplete information was provided.|
|**3709**|**adErrInvalidConnection**|The connection cannot be used to perform this operation. It is either closed or invalid in this context.|
|**3710**|**adErrNotReentrant**|Operation cannot be performed while processing event. An operation cannot be performed within an event handler that causes the event to fire again. For example, navigation methods should not be called from within a **WillMove** event handler.|
|**3711**|**adErrStillExecuting**|Operation cannot be performed while executing asynchronously.|
|**3712**|**adErrOperationCancelled**|Operation has been canceled by the user. The application has called the **CancelUpdate** or **CancelBatch** method and the current operation has been canceled.|
|**3713**|**adErrStillConnecting**|Operation cannot be performed while connecting asynchronously.|
|**3714**|**adErrInvalidTransaction**|Coordinating transaction is invalid or has not started.|
|**3715**|**adErrNotExecuting**|Operation cannot be performed while not executing.|
|**3716**|**adErrUnsafeOperation**|Safety settings on this computer prohibit accessing a data source on another domain.|
|**3717**|**adWrnSecurityDialog**|For internal use only. Don't use. (Entry was included for the sake of completeness. This error should not appear in your code.)|
|**3718**|**adWrnSecurityDialogHeader**|For internal use only. Don't use. (Entry included for the sake of completeness. This error should not appear in your code.)|
|**3719**|**adErrIntegrityViolation**|Data value conflicts with the integrity constraints of the field. A new value for a **Field** would cause a duplicate key. A value that forms one side of a relationship between two records might not be updatable.|
|**3720**|**adErrPermissionDenied**|Insufficient permission prevents writing to the field. The user named in the connection string does not have the proper permissions to write to a **Field**.|
|**3721**|**adErrDataOverflow**|Data value is too large to be represented by the field data type. A numeric value that is too large for the intended field was assigned. For example, a long integer value was assigned to a short integer field.|
|**3722**|**adErrSchemaViolation**|Data value conflicts with the data type or constraints of the field. The data store has validation constraints that differ from the **Field** value.|
|**3723**|**adErrSignMismatch**|Conversion failed because the data value was signed and the field data type used by the provider was unsigned.|
|**3724**|**adErrCantConvertvalue**|Data value cannot be converted for reasons other than sign mismatch or data overflow. For example, conversion would have truncated data.|
|**3725**|**adErrCantCreate**|Data value cannot be set or retrieved because the field data type was unknown, or the provider had insufficient resources to perform the operation.|
|**3726**|**adErrColumnNotOnThisRow**|Record does not contain this field. An incorrect field name was specified or a field not in the **Fields** collection of the current record was referenced.|
|**3727**|**adErrURLDoesNotExist**|Either the source URL or the parent of the destination URL does not exist. There is a typographical error in either the source or destination URL. You might have `https://mysite/photo/myphoto.jpg` when you should actually have `https://mysite/photos/myphoto.jpg` instead. The typographical error in the parent URL (in this case, *photo* instead of *photos*) has caused the error.|
|**3728**|**adErrTreePermissionDenied**|Permissions are insufficient to access tree or subtree. The user named in the connection string does not have the appropriate permissions.|
|**3729**|**adErrInvalidURL**|URL contains invalid characters. Make sure the URL is typed correctly. The URL follows the scheme registered to the current provider (for example, Internet Publishing Provider is registered for http).|
|**3730**|**adErrResourceLocked**|Object represented by the specified URL is locked by one or more other processes. Wait until the process has finished and attempt the operation again. The object you are trying to access has been locked by another user or by another process in your application. This is most likely to arise in a multi-user environment.|
|**3731**|**adErrResourceExists**|Copy operation cannot be performed. Object named by destination URL already exists. Specify **adCopyOverwrite** to replace the object. If you do not specify **adCopyOverwrite** when copying the files in a directory, the copy fails when you try to copy an item that already exists in the destination location.|
|**3732**|**adErrCannotComplete**|The server cannot complete the operation. This might be because the server is busy with other operations or it might be low on resources.|
|**3733**|**adErrVolumeNotFound**|Provider cannot locate the storage device indicated by the URL. Make sure the URL is typed correctly. The URL of the storage device might be incorrect, but this error can occur for other reasons. The device might be offline or a large volume of network traffic might prevent the connection from being made.|
|**3734**|**adErrOutOfSpace**|Operation cannot be performed. Provider cannot obtain enough storage space. There might not be enough RAM or hard-drive space for temporary files on the server.|
|**3735**|**adErrResourceOutOfScope**|Source or destination URL is outside the scope of the current record.|
|**3736**|**adErrUnavailable**|Operation failed to complete and the status is unavailable. The field may be unavailable or the operation was not attempted. Another user might have changed or deleted the field you are trying to access.|
|**3737**|**adErrURLNamedRowDoesNotExist**|Record named by this URL does not exist. While attempting to open a file using a **Record** object, either the file name or the path to the file was misspelled.|
|**3738**|**adErrDelResOutOfScope**|The URL of the object to be deleted is outside the scope of the current record.|
|**3747**|**adErrCatalogNotSet**|Operation requires a valid **ParentCatalog**.|
|**3748**|**adErrCantChangeConnection**|Connection was denied. The new connection you requested has different characteristics than the one already in use.|
|**3749**|**adErrFieldsUpdateFailed**|Fields update failed. For further information, examine the **Status** property of individual field objects. This error can occur in two situations: when changing a **Field** object's value in the process of changing or adding a record to the database; and when changing the properties of the **Field** object itself.<br /><br /> The **Record** or **Recordset** update failed due to a problem with one of the fields in the current record. Enumerate the **Fields** collection and check the **Status** property of each field to determine the cause of the problem.|
|**3750**|**adErrDenyNotSupported**|Provider does not support sharing restrictions. An attempt was made to restrict file sharing and your provider does not support the concept.|
|**3751**|**adErrDenyTypeNotSupported**|Provider does not support the requested kind of sharing restriction. An attempt was made to establish a particular type of file-sharing restriction that is not supported by your provider. See the provider's documentation to determine what file-sharing restrictions are supported.|