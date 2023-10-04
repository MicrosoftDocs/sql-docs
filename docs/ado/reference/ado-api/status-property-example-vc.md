---
title: "Status Property Example (VC++)"
description: "Status Property Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Status property [ADO], VC++ example"
dev_langs:
  - "C++"
---
# Status Property Example (VC++)
This example uses the [Status](./status-property-ado-recordset.md) property to display which records have been modified in a batch operation before a batch update has occurred.  
  
```  
// BeginStatusCpp.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <ole2.h>  
#include <stdio.h>  
#include <conio.h>  
#include "icrsint.h"  
  
// This class extracts title_id and type from titles table.  
class CTitleRs : public CADORecordBinding {  
   BEGIN_ADO_BINDING(CTitleRs)  
      // Column title_id is the 1st field in the table  
      ADO_VARIABLE_LENGTH_ENTRY2(1, adVarChar, m_szt_Title_id, sizeof(m_szt_Title_id), lt_Title_idStatus, FALSE)  
  
      // Column type is the 3rd field in the table  
      ADO_VARIABLE_LENGTH_ENTRY2(3, adVarChar, m_szt_Type, sizeof(m_szt_Type), lt_TypeStatus, TRUE)  
  
END_ADO_BINDING()  
  
public:  
   CHAR m_szt_Title_id[7];  
   ULONG lt_Title_idStatus;  
   CHAR m_szt_Type[13];  
   ULONG lt_TypeStatus;  
};  
  
// Function declarations  
inline void TESTHR(HRESULT x) {if FAILED(x) _com_issue_error(x);};  
void StatusX(void);  
void PrintProviderError(_ConnectionPtr pConnection);  
void PrintComError(_com_error &e);  
  
int main() {  
   if(FAILED(CoInitialize(NULL)))  
      return -1;  
  
   StatusX();  
  
   ::CoUninitialize();  
}  
  
void StatusX() {  
   HRESULT  hr = S_OK;  
  
   // Define string variables.  
   _bstr_t strCnn("Provider='sqloledb'; Data Source='My_Data_Source'; Initial Catalog='pubs'; Integrated Security='SSPI';");  
  
   // Define ADO object pointers.  
   // Initialize pointers on define.  
   // These are in the ADODB::  namespace.  
    IADORecordBinding *picRs = NULL;   // Interface Pointer Declared  
    CTitleRs titlers;   // C++ Class Object  
   _RecordsetPtr pRstTitles = NULL;  
   LPSTR p_TempStr = NULL;  
   char * token1;  
  
   try {  
      // Open recordset for batch update  
      TESTHR(hr = pRstTitles.CreateInstance(__uuidof(Recordset)));  
      pRstTitles->CursorType = adOpenKeyset;  
      pRstTitles->LockType = adLockBatchOptimistic;  
      pRstTitles->Open ("titles", strCnn, adOpenKeyset, adLockBatchOptimistic, adCmdTable);  
  
      // Open an IADORecordBinding interface pointer which   
      // we will use for binding Recordset to a class.  
      TESTHR(hr = pRstTitles->QueryInterface(__uuidof(IADORecordBinding),(LPVOID*)&picRs));  
  
      // Bind the Recordset to a C++ class here   
      TESTHR(hr = picRs->BindToRecordset(&titlers));  
  
      p_TempStr = (LPSTR) malloc(sizeof(titlers.m_szt_Type));  
  
      // Change the type of psychology titles.  
      while (!(pRstTitles->EndOfFile)) {  
         // Set the final character of the destination string to NULL.  
         p_TempStr[sizeof(titlers.m_szt_Type) - 1] = '\0';  
  
         // The source string will get truncated if its length is   
         // longer than the length of the destination string minus one.  
         strncpy_s(p_TempStr,  sizeof(titlers.m_szt_Type), strtok_s(titlers.m_szt_Type, " ", &token1), sizeof(titlers.m_szt_Type) - 1);  
  
         // Compare the type of psychology titles  
         if (!strcmp(p_TempStr,"psychology"))  
               // Copy "self_help" title field  
               pRstTitles->Fields->GetItem("type")->Value = (_bstr_t) ("self_help");  
         pRstTitles->MoveNext();  
      }  
  
      // Display Title ID and status  
      pRstTitles->MoveFirst();  
      while ( !(pRstTitles->EndOfFile) ) {  
         if ( pRstTitles->Status == adRecModified )  
            printf("%s - Modified\n",titlers.lt_Title_idStatus == adFldOK ? titlers.m_szt_Title_id : "<NULL>");  
         else  
            printf("%s\n",titlers.lt_Title_idStatus == adFldOK ? titlers.m_szt_Title_id : "<NULL>");  
         pRstTitles->MoveNext();  
      }  
   }  
  
   catch (_com_error &e) {  
      // Notify the user of errors if any.  
      // Pass a connection pointer accessed from the Recordset.  
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
  
   // Deallocate the memory  
   if (p_TempStr)  
      free(p_TempStr);  
  
   if (pRstTitles)  
      if (pRstTitles->State == adStateOpen) {  
         // Cancel the update because this is a demonstration.  
         pRstTitles->CancelBatch(adAffectAll);  
         pRstTitles->Close();  
      }  
}  
  
void PrintProviderError(_ConnectionPtr pConnection) {  
   // Print Provider Errors from Connection object.  
   // pErr is a record object in the Connection's Error collection.  
   ErrorPtr pErr = NULL;  
  
   if ( (pConnection->Errors->Count) > 0) {  
      long nCount = pConnection->Errors->Count;  
  
      // Collection ranges from 0 to nCount -1.  
      for (long i = 0; i < nCount; i++) {  
         pErr = pConnection->Errors->GetItem(i);  
         printf("Error number: %x\t%s\n", pErr->Number, (LPCSTR) pErr->Description);  
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
 [Status Property (ADO Recordset)](./status-property-ado-recordset.md)