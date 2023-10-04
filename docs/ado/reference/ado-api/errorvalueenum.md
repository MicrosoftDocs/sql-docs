---
title: "ErrorValueEnum"
description: "ErrorValueEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ErrorValueEnum"
helpviewer_keywords:
  - "ErrorValueEnum enumeration [ADO]"
apitype: "COM"
---
# ErrorValueEnum
Specifies the type of ADO run-time error.  
  
 Three forms of the error number are listed:  
  
-   Positive decimal-The low two bytes of the full number in decimal format. This number is displayed in the default Visual Basic error message dialog box. For example, Run-time error '3707'.  
  
-   Negative decimal-The decimal translation of the full error number.  
  
-   Hexadecimal-The hexadecimal representation of the full error number. The Windows facility code is in the fourth digit. The facility code for ADO error numbers is *A*. For example: 0x800***A***0E7B.  
  
> [!NOTE]
>  OLE DB errors may be passed to your ADO application. Typically, these can be identified by a Windows facility code of *4*. For example, 0x800***4***.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adErrBoundToCommand**|3707 -2146824581 0x800A0E7B|Cannot change the **ActiveConnection** property of a **Recordset** object that has a **Command** object as its source.|  
|**adErrCannotComplete**|3732 -2146824556 0x800A0E94|Server cannot complete the operation.|  
|**adErrCantChangeConnection**|3748 -2146824540 0x800A0EA4|Connection was denied. New connection you requested has different characteristics than the one already being used.|  
|**adErrCantChangeProvider**|3220 -2146825068 0X800A0C94|Supplied provider differs from the one already being used.|  
|**adErrCantConvertvalue**|3724 -2146824564 0x800A0E8C|Data value cannot be converted for reasons other than sign mismatch or data overflow. For example, conversion would have truncated data.|  
|**adErrCantCreate**|3725 -2146824563 0x800A0E8D|Data value cannot be set or retrieved because the field data type was unknown, or the provider had insufficient resources to perform the operation.|  
|**adErrCatalogNotSet**|3747 -2146824541 0x800A0EA3|Operation requires a valid **ParentCatalog**.|  
|**adErrColumnNotOnThisRow**|3726 -2146824562 0x800A0E8E|Record does not contain this field.|  
|**adErrDataConversion**|3421 -2146824867 0x800A0D5D|Application uses a value of the wrong type for the current operation.|  
|**adErrDataOverflow**|3721 -2146824567 0x800A0E89|Data value is too large to be represented by the field data type.|  
|**adErrDelResOutOfScope**|3738 -2146824550 0x800A0E9A|URL of the object to be deleted is outside the scope of the current record.|  
|**adErrDenyNotSupported**|3750 -2146824538 0x800A0EA6|Provider does not support sharing restrictions.|  
|**adErrDenyTypeNotSupported**|3751 -2146824537 0x800A0EA7|Provider does not support the requested kind of sharing restriction.|  
|**adErrFeatureNotAvailable**|3251 -2146825037 0x800A0CB3|Object or provider is not able to perform requested operation.|  
|**adErrFieldsUpdateFailed**|3749 -2146824539 0x800A0EA5|Fields update failed. For more information, examine the **Status** property of individual field objects.|  
|**adErrIllegalOperation**|3219 -2146825069 0x800A0C93|Operation is not allowed in this context.|  
|**adErrIntegrityViolation**|3719 -2146824569 0x800A0E87|Data value conflicts with the integrity constraints of the field.|  
|**adErrInTransaction**|3246 -2146825042 0x800A0CAE|**Connection** object cannot be explicitly closed while in a transaction.|  
|**adErrInvalidArgument**|3001 -2146825287 0x800A0BB9|Arguments are of the wrong type, are out of acceptable range, or are in conflict with one another.|  
|**adErrInvalidConnection**|3709 -2146824579 0x800A0E7D|The connection cannot be used to perform this operation. It is either closed or invalid in this context.|  
|**adErrInvalidParamInfo**|3708 -2146824580 0x800A0E7C|**Parameter** object is incorrectly defined. Inconsistent or incomplete information was provided.|  
|**adErrInvalidTransaction**|3714 -2146824574 0x800A0E82|Coordinating transaction is invalid or has not started.|  
|**adErrInvalidURL**|3729 -2146824559 0x800A0E91|URL contains invalid characters. Make sure that the URL is typed correctly.|  
|**adErrItemNotFound**|3265 -2146825023 0x800A0CC1|Item cannot be found in the collection that corresponds to the requested name or ordinal.|  
|**adErrNoCurrentRecord**|3021 -2146825267 0x800A0BCD|Either **BOF** or **EOF** is True, or the current record has been deleted. Requested operation requires a current record.|  
|**adErrNotExecuting**|3715 -2146824573 0x800A0E83|Operation cannot be performed while not executing.|  
|**adErrNotReentrant**|3710 -2146824578 0x800A0E7E|Operation cannot be performed while processing event.|  
|**adErrObjectClosed**|3704 -2146824584 0x800A0E78|Operation is not allowed when the object is closed.|  
|**adErrObjectInCollection**|3367 -2146824921 0x800A0D27|Object is already in collection. Cannot append.|  
|**adErrObjectNotSet**|3420 -2146824868 0x800A0D5C|Object is no longer valid.|  
|**adErrObjectOpen**|3705 -2146824583 0x800A0E79|Operation is not allowed when the object is open.|  
|**adErrOpeningFile**|3002 -2146825286 0x800A0BBA|File could not be opened.|  
|**adErrOperationCancelled**|3712 -2146824576 0x800A0E80|Operation has been canceled by the user.|  
|**adErrOutOfSpace**|3734 -2146824554 0x800A0E96|Operation cannot be performed. Provider cannot obtain enough storage space.|  
|**adErrPermissionDenied**|3720 -2146824568 0x800A0E88|Insufficient permission prevents writing to the field.|  
|**adErrProviderFailed**|3000 -2146825288 0x800A0BB8|Provider did not perform the requested operation.|  
|**adErrProviderNotFound**|3706 -2146824582 0x800A0E7A|Provider cannot be found. It may not be correctly installed.|  
|**adErrReadFile**|3003 -2146825285 0x800A0BBB|File could not be read.|  
|**adErrResourceExists**|3731 -2146824557 0x800A0E93|Copy operation cannot be performed. Object named by destination URL already exists. Specify **adCopyOverwrite** to replace the object.|  
|**adErrResourceLocked**|3730 -2146824558 0x800A0E92|Object represented by the specified URL is locked by one or more other processes. Wait until the process has finished and try the operation again.|  
|**adErrResourceOutOfScope**|3735 -2146824553 0x800A0E97|Source or destination URL is outside the scope of the current record.|  
|**adErrSchemaViolation**|3722 -2146824566 0x800A0E8A|Data value conflicts with the data type or constraints of the field.|  
|**adErrSignMismatch**|3723 -2146824565 0x800A0E8B|Conversion failed because the data value was signed and the field data type used by the provider was unsigned.|  
|**adErrStillConnecting**|3713 -2146824575 0x800A0E81|Operation cannot be performed while connecting asynchronously.|  
|**adErrStillExecuting**|3711 -2146824577 0x800A0E7F|Operation cannot be performed while executing asynchronously.|  
|**adErrTreePermissionDenied**|3728 -2146824560 0x800A0E90|Permissions are insufficient to access tree or subtree.|  
|**adErrUnavailable**|3736 -2146824552 0x800A0E98|Operation did not complete and the status is unavailable. The field may be unavailable or the operation was not attempted.|  
|**adErrUnsafeOperation**|3716 -2146824572 0x800A0E84|Safety settings on this computer prevent accessing a data source on another domain.|  
|**adErrURLDoesNotExist**|3727 -2146824561 0x800A0E8F|Either the source URL or the parent of the destination URL does not exist.|  
|**adErrURLNamedRowDoesNotExist**|3737 -2146824551 0x800A0E99|Record named by this URL does not exist.|  
|**adErrVolumeNotFound**|3733 -2146824555 0x800A0E95|Provider cannot locate the storage device indicated by the URL. Make sure that the URL is typed correctly.|  
|**adErrWriteFile**|3004 -2146825284 0x800A0BBC|Write to file failed.|  
|**adWrnSecurityDialog**|3717 -2146824571 0x800A0E85|For internal use only. Do not use.|  
|**adWrnSecurityDialogHeader**|3718 -2146824570 0x800A0E86|For internal use only. Do not use.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
 Only the following subsets of ADO/WFC equivalents are defined.  
  
|Constant|  
|--------------|  
|AdoEnums.ErrorValue.BOUNDTOCOMMAND|  
|AdoEnums.ErrorValue.DATACONVERSION|  
|AdoEnums.ErrorValue.FEATURENOTAVAILABLE|  
|AdoEnums.ErrorValue.ILLEGALOPERATION|  
|AdoEnums.ErrorValue.INTRANSACTION|  
|AdoEnums.ErrorValue.INVALIDARGUMENT|  
|AdoEnums.ErrorValue.INVALIDCONNECTION|  
|AdoEnums.ErrorValue.INVALIDPARAMINFO|  
|AdoEnums.ErrorValue.ITEMNOTFOUND|  
|AdoEnums.ErrorValue.NOCURRENTRECORD|  
|AdoEnums.ErrorValue.NOTEXECUTING|  
|AdoEnums.ErrorValue.NOTREENTRANT|  
|AdoEnums.ErrorValue.OBJECTCLOSED|  
|AdoEnums.ErrorValue.OBJECTINCOLLECTION|  
|AdoEnums.ErrorValue.OBJECTNOTSET|  
|AdoEnums.ErrorValue.OBJECTOPEN|  
|AdoEnums.ErrorValue.OPERATIONCANCELLED|  
|AdoEnums.ErrorValue.PROVIDERNOTFOUND|  
|AdoEnums.ErrorValue.STILLCONNECTING|  
|AdoEnums.ErrorValue.STILLEXECUTING|  
|AdoEnums.ErrorValue.UNSAFEOPERATION|  
  
## Applies To  
 [Number Property (ADO)](../../../ado/reference/ado-api/number-property-ado.md)  
  
## See Also  
 [ADO Error Codes](../../../ado/guide/appendixes/ado-error-codes.md)
