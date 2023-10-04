---
title: "Resync Method Example (VC++)"
description: "Resync Method Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Resync method [ADO], VC++ example"
dev_langs:
  - "C++"
---
# Resync Method Example (VC++)
This example demonstrates using the [Resync](./resync-method.md) method to refresh data in a static recordset.  
  
```  
// Resync_Method_Sample.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <ole2.h>  
#include <stdio.h>  
#include <conio.h>  
  
// Function declarations  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
void ResyncX();  
void PrintProviderError(_ConnectionPtr pConnection);  
void PrintComError(_com_error &e);  
  
int main() {  
   if ( FAILED(::CoInitialize(NULL)) )  
      return -1;  
  
   ResyncX();  
   ::CoUninitialize();  
}  
  
void ResyncX() {  
   // Define string variables.  
   _bstr_t strCnn("Provider='sqloledb'; Data Source='My_Data_Source'; Initial Catalog='pubs'; Integrated Security='SSPI';");  
  
   // Define ADO object pointers.  Initialize pointers on define.  
   // These are in the ADODB:: namespace.  
   _RecordsetPtr pRstTitles = NULL;  
  
   try {  
      // Open recordset for titles table.  
      TESTHR(pRstTitles.CreateInstance(__uuidof(Recordset)));  
      pRstTitles->CursorLocation = adUseClient;  
      pRstTitles->CursorType = adOpenStatic;  
      pRstTitles->LockType = adLockBatchOptimistic;  
      pRstTitles->Open ("titles", strCnn, adOpenStatic, adLockBatchOptimistic, adCmdTable);  
  
      // Change the type of the first title in the recordset.  
      pRstTitles->Fields->GetItem("type")->Value = (_bstr_t) ("database");  
  
      // Display the results of the change.  
      printf("\nBefore resync: \n\n");  
  
      printf("Title - %s\n\n",   
         (LPSTR)(_bstr_t) pRstTitles->Fields->GetItem("title")->Value);  
  
      printf("Type - %s\n\n",  
         (LPSTR)(_bstr_t) pRstTitles->Fields->GetItem("type")->Value);  
  
      // Resync with database.  
      pRstTitles->Resync(adAffectAll,adResyncAllValues);  
  
      // Display the results of the resynch.  
      printf("\n\nAfter resync: \n\n");  
  
      printf("Title - %s\n\n",  
         (LPSTR)(_bstr_t) pRstTitles->Fields->GetItem("title")->Value);  
  
      printf("Type - %s\n\n",  
         (LPSTR)(_bstr_t) pRstTitles->Fields->GetItem("type")->Value);  
   }  
   catch (_com_error &e) {  
      // Display errors, if any. Pass a connection pointer accessed from the Recordset.  
      _variant_t vtConnect = pRstTitles->GetActiveConnection();  
  
      // GetActiveConnection returns connect string if connection  
      // is not open, else returns Connection object.  
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
  
   if (pRstTitles)  
      if (pRstTitles->State == adStateOpen) {  
         pRstTitles->CancelBatch(adAffectAll);  
         pRstTitles->Close();  
      }  
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
         printf("\t Error number: %x\t%s\n", pErr->Number, (LPCSTR) pErr->Description);  
      }  
   }  
}  
  
void PrintComError(_com_error &e) {  
   _bstr_t bstrSource(e.Source());  
   _bstr_t bstrDescription(e.Description());  
  
   // Print COM errors.   
   printf("Error\n");  
   printf("\tCode = %08lx\n", e.Error());  
   printf("\tCode meaning = %s\n", e.ErrorMessage());  
   printf("\tSource = %s\n", (LPCSTR) bstrSource);  
   printf("\tDescription = %s\n", (LPCSTR) bstrDescription);  
}  
```  
  
## See Also  
 [Resync Method](./resync-method.md)