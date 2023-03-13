---
title: "Count Property Example (VC++)"
description: "Count Property Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Count property [ADO], VC++ example"
dev_langs:
  - "C++"
---
# Count Property Example (VC++)
This example demonstrates the [Count](./count-property-ado.md) property with two collections in the ***Employee*** database. The property obtains the number of objects in each collection, and sets the upper limit for loops that enumerate these collections.  
  
```  
// BeginCountCpp.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <ole2.h>  
#include <stdio.h>  
#include<conio.h>  
  
// Function declarations  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
void CountX();  
void PrintProviderError(_ConnectionPtr pConnection);  
void PrintComError(_com_error &e);  
  
int main() {  
   if ( FAILED(::CoInitialize(NULL)) )  
      return -1;  
   CountX();  
   ::CoUninitialize();  
}  
  
void CountX() {  
   // Define ADO object pointers.  Initialize pointers on define.  
   // These are in the ADODB::  namespace  
   _RecordsetPtr pRstEmployee = NULL;  
  
   // Define Other Variables  
   _variant_t Index;  
   Index.vt = VT_I2;  
   int j = 0;  
  
   _bstr_t strCnn("Provider='sqloledb'; Data Source='My_Data_Source'; Initial Catalog='pubs'; Integrated Security='SSPI';");  
  
   try {  
      // Open recordset with data from Employee table.  
      TESTHR(pRstEmployee.CreateInstance(__uuidof(Recordset)));  
      pRstEmployee->Open("Employee", strCnn, adOpenForwardOnly, adLockReadOnly, adCmdTable);  
  
      // Print information about Fields collection.  
      printf("%d Fields in Employee\n", pRstEmployee->Fields->Count);  
      for (short int intLoop = 0 ; intLoop <= (pRstEmployee->Fields->Count-1) ; intLoop++) {  
         Index.iVal = intLoop;  
         printf(" %s\n", (LPSTR) pRstEmployee->Fields->GetItem(Index)->Name);  
      }  
  
      // Print information about Properties collection.  
      printf("\n%d Properties in Employee\n", pRstEmployee->Properties->Count);  
      for (short int intLoop = 0 ; intLoop <= (pRstEmployee->Properties->Count - 1) ; intLoop++) {  
         j++;  
         Index.iVal = intLoop;  
         printf(" %s\n" , (LPSTR)pRstEmployee->Properties->GetItem(Index)->Name);  
      }  
   }  
   catch(_com_error &e) {  
      // Display any errors that result from executing the query.  
      // Pass a connection pointer accessed from the Recordset.  
      _variant_t vtConnect = pRstEmployee->GetActiveConnection();  
  
      switch(vtConnect.vt) {  
      case VT_BSTR:  
         PrintComError(e);  
         break;  
      case VT_DISPATCH:  
         PrintProviderError(vtConnect);  
         break;  
      default:  
         printf("Errors occured.");  
         break;  
      }  
   }  
   // Clean up objects before exit.  
   if (pRstEmployee)  
      if (pRstEmployee->State == adStateOpen)  
         pRstEmployee->Close();  
}  
  
void PrintProviderError(_ConnectionPtr pConnection) {  
   // Print Provider Errors from Connection object.  
   // pErr is a record object in the Connection's Error collection.  
   ErrorPtr pErr = NULL;  
  
   if ( (pConnection->Errors->Count) > 0 ) {  
      long nCount = pConnection->Errors->Count;  
  
      // Collection ranges from 0 to nCount -1.  
      for ( long i = 0 ; i < nCount ; i++ ) {  
         pErr = pConnection->Errors->GetItem(i);  
         printf("Error number: %x\t%s\n", pErr->Number, pErr->Description);  
      }  
   }  
}  
  
void PrintComError(_com_error &e) {  
   _bstr_t bstrSource(e.Source());  
   _bstr_t bstrDescription(e.Description());  
  
   // Print Com errors.  
   printf("Error\n");  
   printf("\tCode = %08lx\n", e.Error());  
   printf("\tCode meaning = %s\n", e.ErrorMessage());  
   printf("\tSource = %s\n", (LPCSTR) bstrSource);  
   printf("\tDescription = %s\n", (LPCSTR) bstrDescription);  
}  
```  
  
## See Also  
 [Count Property (ADO)](./count-property-ado.md)