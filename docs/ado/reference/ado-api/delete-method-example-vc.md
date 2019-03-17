---
title: "Delete Method Example (VC++) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "Delete method [ADO], VC++ example"
ms.assetid: 7cc78fb5-2701-49dc-bc22-06613b10cecb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Delete Method Example (VC++)
This example uses the [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) method to remove a specified record from a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
## Example  
  
```  
// DeleteMethodExample.cpp  
// compile with: /EHsc /c  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <stdio.h>  
#include <ole2.h>  
#include <conio.h>  
#include "icrsint.h"  
  
// This Class extracts titleid, lorange, hirange and royalty from the "roysched" table.  
class CRoySchedRs : public CADORecordBinding {  
   BEGIN_ADO_BINDING(CRoySchedRs)  
  
      // Column empid is the 1st field in the recordset     
  
      ADO_VARIABLE_LENGTH_ENTRY2(1, adVarChar, m_sz_titleid,   
      sizeof(m_sz_titleid), lemp_titleidStatus, TRUE)  
  
      ADO_VARIABLE_LENGTH_ENTRY2(2, adInteger, m_sz_lorange,   
      sizeof(m_sz_lorange), lemp_lorangeStatus, TRUE)  
  
      ADO_VARIABLE_LENGTH_ENTRY2(3, adInteger, m_sz_hirange,   
      sizeof(m_sz_hirange), lemp_hirangeStatus, TRUE)  
  
      ADO_VARIABLE_LENGTH_ENTRY2(4, adInteger, m_sz_royalty,   
      sizeof(m_sz_royalty), lemp_royaltyStatus, TRUE)  
  
   END_ADO_BINDING()  
  
public:  
   CHAR   m_sz_titleid[10];  
   ULONG   lemp_titleidStatus;  
   ULONG   m_sz_lorange;  
   ULONG   lemp_lorangeStatus;  
   ULONG   m_sz_hirange;  
   ULONG   lemp_hirangeStatus;  
   ULONG   m_sz_royalty;  
   ULONG   lemp_royaltyStatus;  
};  
  
// Function Declarations.  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
void DeleteX();  
void PrintProviderError(_ConnectionPtr pConnection);  
void PrintComError(_com_error &e);  
inline char* mygets(char* strDest, int n) {      
   char strExBuff[10];  
   char* pstrRet = fgets(strDest, n, stdin);  
  
   if (pstrRet == NULL)  
      return NULL;  
  
   if (!strrchr(strDest, '\n'))  
      // Exhaust the input buffer.  
      do {  
         fgets(strExBuff, sizeof(strExBuff), stdin);  
      } while (!strrchr(strExBuff, '\n'));  
   else  
      // Replace '\n' with '\0'  
      strDest[strrchr(strDest, '\n') - strDest] = '\0';  
  
   return pstrRet;  
}  
  
int main() {  
   if ( FAILED(::CoInitialize(NULL)) )  
      return -1;  
  
   DeleteX();  
   ::CoUninitialize();  
}  
  
void DeleteX() {  
   // Define ADO object pointers.  Initialize pointers on define.  
   // These are in the ADODB::  namespace.  
   _RecordsetPtr pRstRoySched = NULL;  
  
   // Define Other Variables  
   IADORecordBinding *picRs = NULL;   // Interface Pointer declared.  
   CRoySchedRs royschrs;   // C++ class object  
  
   char strTitleID[10], strTmpTitleID[10] = "";  
   long longHiRange = 0;  
   int intRoyalty = 0, intLoRange = 0, cnt = 0;  
   bool blnFound = TRUE;  
  
   _bstr_t strCnn("Provider='sqloledb'; Data Source='My_Data_Source'; Initial Catalog='pubs'; Integrated Security='SSPI';");  
  
   try {  
      // Open RoySched table  
      TESTHR(pRstRoySched.CreateInstance(__uuidof(Recordset)));  
  
      pRstRoySched->CursorLocation = adUseClient;  
      pRstRoySched->CursorType = adOpenStatic;  
      pRstRoySched->LockType = adLockBatchOptimistic;  
  
      TESTHR(pRstRoySched->Open("SELECT * FROM roysched WHERE royalty = 20",  
         strCnn, adOpenStatic, adLockBatchOptimistic, adCmdText));  
  
      // Prompt for a record to delete.  
      printf("Before delete there are %d titles with 20 percent royalty :\n",   
         pRstRoySched->RecordCount);  
  
      // Open an IADORecordBinding interface pointer for Binding Recordset to a class.  
      TESTHR(pRstRoySched->QueryInterface(__uuidof(IADORecordBinding), (LPVOID*)&picRs));  
  
      // Bind the Recordset to a C++ Class here      
      TESTHR(picRs->BindToRecordset(&royschrs));  
  
      while(!(pRstRoySched->EndOfFile)) {  
         printf("%s\n", royschrs.lemp_titleidStatus == adFldOK ?   
            royschrs.m_sz_titleid : "<NULL>");  
         pRstRoySched->MoveNext();  
      }  
  
      printf("\nEnter the ID of a record to delete: ");  
      mygets(strTitleID, 10);  
  
      // Converting the title_id to upper case  
      for ( cnt = 0 ; cnt < 10 ; cnt++ )  
         if ( strTitleID[cnt] != NULL )  
            if ( IsCharAlpha( strTitleID[cnt]) )  
               if ( islower( strTitleID[cnt]) )  
                  strTmpTitleID[cnt] = _toupper(strTitleID[cnt]);  
               else  
                  strTmpTitleID[cnt] = strTitleID[cnt];  
            else                  
               strTmpTitleID[cnt] = strTitleID[cnt];              
  
      // Move to the record and save data so it can be restored.  
      pRstRoySched->PutFilter ("title_id = '" +   
         (_bstr_t)(LPCSTR)strTmpTitleID + "'");  
  
       if ( pRstRoySched->RecordCount != 0 ) {  
         intLoRange = royschrs.m_sz_lorange;  
         longHiRange = royschrs.m_sz_hirange;  
         intRoyalty = royschrs.m_sz_royalty;  
  
         // Delete the record  
         pRstRoySched->Delete(adAffectCurrent);  
         pRstRoySched->UpdateBatch(adAffectCurrent);  
      }  
      else {  
         blnFound = FALSE;  
         printf("This Title ID not available");  
      }  
  
      // Show the results.  
      VARIANT varFilter;  
      varFilter.vt = VT_I2;  
      varFilter.iVal = adFilterNone;  
      pRstRoySched->PutFilter (varFilter);  
      pRstRoySched->Requery(-1);  
  
      // Bind the Recordset to a C++ Class here.  
      TESTHR(picRs->BindToRecordset(&royschrs));  
  
      printf("\nAfter delete there are %d titles with 20 percent royalty: ",   
         pRstRoySched->RecordCount);  
  
      while ( !(pRstRoySched->EndOfFile) ) {  
         printf("\n%s", royschrs.lemp_titleidStatus == adFldOK ?  
            royschrs.m_sz_titleid : "<NULL>");  
         pRstRoySched->MoveNext();  
      }  
  
      // Restore the data because this is a demonstration.  
      if ( blnFound ) {  
         // Set the final character of the destination string to NULL.  
         royschrs.m_sz_titleid[sizeof(royschrs.m_sz_titleid) - 1] = '\0';  
         // The source string will get truncated if its length is   
         // longer than the length of the destination string minus one.  
         strncpy_s(royschrs.m_sz_titleid, strTmpTitleID, sizeof(royschrs.m_sz_titleid) - 1);  
         royschrs.m_sz_lorange = intLoRange;  
         royschrs.m_sz_hirange = longHiRange;  
         royschrs.m_sz_royalty = intRoyalty;  
  
         TESTHR(picRs->AddNew(&royschrs));  
  
         pRstRoySched->UpdateBatch(adAffectCurrent);  
      }  
   }  
   catch(_com_error &e) {  
      // Display errors, if any.  Pass connection pointer accessed from the Recordset.  
      _variant_t vtConnect = pRstRoySched->GetActiveConnection();  
  
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
  
   // Clean up objects before exit. Release the IADORecordset Interface here     
   if (picRs)  
      picRs->Release();  
  
   if (pRstRoySched)  
      if (pRstRoySched->State == adStateOpen)  
         pRstRoySched->Close();  
}  
  
void PrintProviderError(_ConnectionPtr pConnection) {  
   // Print Provider Errors from Connection object.  
   // pErr is a record object in the Connection's Error collection.  
   ErrorPtr pErr = NULL;  
  
   if ( (pConnection->Errors->Count) > 0) {  
      long nCount = pConnection->Errors->Count;  
      // Collection ranges from 0 to nCount -1.  
      for ( long i = 0 ; i < nCount ; i++ ) {  
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
  
## Sample Input  
  
```  
TC4203  
```  
  
## Sample Output  
  
```  
Before delete there are 7 titles with 20 percent royalty :  
BU2075  
MC3021  
TC3218  
BU1111  
MC2222  
TC4203  
BU7832  
  
Enter the ID of a record to delete:   
After delete there are 6 titles with 20 percent royalty:   
BU2075  
MC3021  
TC3218  
BU1111  
MC2222  
BU7832  
```  
  
## See Also  
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)   
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
