---
title: "SQLRateConnection Function | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLRateConnection function [ODBC]"
ms.assetid: e8da2ffb-d6ef-4ca7-824f-57afd29585d8
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLRateConnection Function
**Conformance**  
 Version Introduced: ODBC 3.81 Standards Compliance: ODBC  
  
 **Summary**  
 **SQLRateConnection** determines if a driver can reuse an existing connection in the connection pool.  
  
## Syntax  
  
```  
SQLRETURN  SQLRateConnection(  
                SQLHDBC_INFO_TOKEN   hRequest,  
                SQLHDBC              hCandidateConnection,  
                BOOL                 fRequiredTransactionEnlistment,  
                TRANSID              transId,  
                DWORD *              pRating );  
```  
  
## Arguments  
 *hRequest*  
 [Input] A token handle representing the new application connection request.  
  
 *hCandidateConnection*  
 [Input] The existing connection in the connection pool. The connection must be in an opened state.  
  
 *fRequiredTransactionEnlistment*  
 [Input] If TRUE, reusing the existing connection's *hCandidateConnection* for the new connection request (*hRequest*) requires an additional enlistment.  
  
 *transId*  
 [Input] If *fRequiredTransactionEnlistment* is TRUE, *transId* represents the DTC transaction that the request will enlist. If *fRequiredTransactionEnlistment* is FALSE, *transId* will be ignored.  
  
 *pRating*  
 [Output] *hCandidateConnection*'s reuse rating for the *hRequest*. This rating will be in between 0 and 100 (inclusive).  
  
## Returns  
 SQL_SUCCESS, SQL_ERROR, or SQL_INVALID_HANDLE.  
  
## Diagnostics  
 The Driver Manager will not process diagnostic information returned from this function.  
  
## Remarks  
 **SQLRateConnection** produces a score between 0 and 100 (inclusive) indicating how well an existing connection matches the request.  
  
|Score|Meaning (when SQL_SUCCESS is returned)|  
|-----------|-----------------------------------------------|  
|0|*hCandidateConnection* must not be reused for the *hRequest*.|  
|Any values between 1 and 98 (inclusive)|The higher the score, the closer that *hCandidateConnection* match with *hRequest*.|  
|99|There are only mismatches in insignificant attributes.  The Driver Manager should stop the rating loop.|  
|100|Perfect match.  The Driver Manager should stop the rating loop.|  
|Any other value greater than 100|*hCandidateConnection* is marked as dead and it will not be reused even in an future connection request.|  
  
 The Driver Manager will mark a connection as dead if the return code is anything other than SQL_SUCCESS (including SQL_SUCCESS_WITH_INFO) or the rating is greater than 100. That dead connection will not be reused (even in future connection requests) and will eventually be timed out after CPTimeout passes. The Driver Manager will continue to find another connection from the pool to rate.  
  
 If the Driver Manager reused a connection whose score is strictly smaller than 100 (including 99), the Driver Manager will call SQLSetConnectAttr(SQL_ATTR_DBC_INFO_TOKEN) to reset the connection back into the state requested by the application. The driver should not reset the connection in this function call.  
  
 If *fRequiredTransactionEnlistment* is TRUE, reusing *hCandidateConnection* needs an extra enlistment (*transId* != NULL) or unenlistment (*transId* == NULL). This indicates the cost of reusing a connection and whether the driver should enlist / unenlist the connection if it is going to reuse the connection. If *fRequireTransactionEnlistment* is FALSE, driver should ignore the value of *transId*.  
  
 The Driver Manager guarantees that the parent HENV handle of *hRequest* and *hCandidateConnection* are the same. The Driver Manager guarantees that the pool ID associated with *hRequest* and *hCandidateConnection* are the same.  
  
 Applications should not call this function directly. An ODBC driver that supports driver-aware connection pooling must implement this function.  
  
 Include sqlspi.h for ODBC driver development.  
  
## See Also  
 [Developing an ODBC Driver](../../../odbc/reference/develop-driver/developing-an-odbc-driver.md)   
 [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md)   
 [Developing Connection-Pool Awareness in an ODBC Driver](../../../odbc/reference/develop-driver/developing-connection-pool-awareness-in-an-odbc-driver.md)
