---
title: "Move Method Example (VC++) | Microsoft Docs"
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
  - "Move method [ADO], VC++ example"
ms.assetid: 0e08af60-f668-4092-8b6a-9e8b6db90448
author: MightyPen
ms.author: genemi
manager: craigg
---
# Move Method Example (VC++)
This example uses the [Move](../../../ado/reference/ado-api/move-method-ado.md) method to position the record pointer based on user input.  
  
## Example  
  
```  
// BeginMoveCpp.cpp  
// compile with: /EHsc /c  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <stdio.h>  
#include <ole2.h>  
#include <stdlib.h>  
#include <conio.h>  
#include "icrsint.h"  
  
// This Class extracts fname, lname, city and state from the "authors" table.  
class CAuthorsRs : public CADORecordBinding {  
   BEGIN_ADO_BINDING(CAuthorsRs)  
  
   // Column au_id is the 1st field in the recordset     
   ADO_VARIABLE_LENGTH_ENTRY2(2, adVarChar, m_au_fname, sizeof(m_au_fname), lemp_fnameStatus, TRUE)  
   ADO_VARIABLE_LENGTH_ENTRY2(3, adVarChar, m_au_lname, sizeof(m_au_lname), lemp_lnameStatus, TRUE)  
   ADO_VARIABLE_LENGTH_ENTRY2(4, adVarChar, m_au_city, sizeof(m_au_city), lemp_cityStatus, TRUE)  
   ADO_VARIABLE_LENGTH_ENTRY2(5, adChar, m_au_state, sizeof(m_au_state), lemp_stateStatus, TRUE)  
   END_ADO_BINDING()  
  
public:  
   char m_au_fname[21];  
   ULONG lemp_fnameStatus;  
   char m_au_lname[41];  
   ULONG lemp_lnameStatus;  
   char m_au_city[21];  
   ULONG lemp_cityStatus;  
   char m_au_state[3];  
   ULONG lemp_stateStatus;  
};  
  
// Function Declaration.  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
void MoveX();  
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
   if (FAILED(::CoInitialize(NULL)))  
      return -1;  
  
   MoveX();  
   ::CoUninitialize();  
}  
  
void MoveX() {  
   // Define ADO object pointers.  Initialize pointers on define.  These are in the ADODB::  namespace.  
   _RecordsetPtr pRstAuthors = NULL;  
   char *token1;  
  
   // Define Other Variables  
   IADORecordBinding *picRs = NULL;   // Interface Pointer declared    
   CAuthorsRs authorsrs;   // C++ class object  
   HRESULT hr = S_OK;  
  
   // Open Authors table  
   _bstr_t strCnn("Provider='sqloledb'; Data Source='My_Data_Source'; Initial Catalog='pubs'; Integrated Security='SSPI';");  
  
   try {  
      // Open recordset from Authors table.  
      TESTHR(pRstAuthors.CreateInstance(__uuidof(Recordset)));  
  
      pRstAuthors->CursorType = adOpenStatic;  
      // Use client cursor to allow use of AbsolutePosition property.  
      pRstAuthors->CursorLocation = adUseClient;  
  
      pRstAuthors->Open("SELECT au_id, au_fname, au_lname, city, "  
         "state FROM Authors ORDER BY au_lname", strCnn, adOpenStatic, adLockReadOnly, adCmdText);  
  
      // Open an IADORecordBinding interface pointer which we'll use   
      // for Binding Recordset to a class.  
      TESTHR(pRstAuthors->QueryInterface(__uuidof(IADORecordBinding),(LPVOID*)&picRs));  
  
      // Bind the Recordset to a C++ Class here.  
      TESTHR(picRs->BindToRecordset(&authorsrs));  
  
      pRstAuthors->MoveFirst();  
  
      char * strMove;  
      char strTemp[5];  
      while (true) {  
         // Display information about current record and ask how many records to move.  
         printf("Record %ld of %d\n", pRstAuthors->AbsolutePosition, pRstAuthors->RecordCount);  
         printf("Author: %s %s\n",   
            authorsrs.lemp_fnameStatus == adFldOK ?   
            authorsrs.m_au_fname : "<NULL>",   
            authorsrs.lemp_lnameStatus == adFldOK ?   
            authorsrs.m_au_lname : "<NULL>");  
         printf("Location: %s, %s\n\n",   
            authorsrs.lemp_cityStatus == adFldOK ?   
            authorsrs.m_au_city : "<NULL>",   
            authorsrs.lemp_stateStatus == adFldOK ?   
            authorsrs.m_au_state : "<NULL>");  
  
         printf("Enter number of records to Move \n(positive or negative, 0 (zero) to quit): ");  
         mygets(strTemp, 5);  
  
         strMove = strtok_s(strTemp, "\t", &token1);  
  
         if (strMove == NULL || *strMove == '0')  
            break;  
  
         // if the input is not numeric then notify the user.  
         if (!atol(strMove)) {  
            printf("Expecting numeric value...\n");  
            continue;  
         }  
  
         // Store bookmark in case the Move goes too far forward or backward.  
         _variant_t varBookmark = pRstAuthors->Bookmark;  
  
         // Move method requires parameter of data type Long.  
         long lngMove = atol(strMove);  
  
         pRstAuthors->Move(lngMove);  
  
         // Trap for BOF or EOF.  
         if (pRstAuthors->BOF) {  
            printf("Too far backward! Returning to current record.\n");  
            pRstAuthors->Bookmark = varBookmark;  
         }  
  
         if (pRstAuthors->EndOfFile) {  
            printf("Too far forward! Returning to current record.\n");  
            pRstAuthors->Bookmark = varBookmark;  
         }  
      }  
   }  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      // Pass a connection pointer accessed from the Recordset.  
      _variant_t vtConnect = pRstAuthors->GetActiveConnection();  
  
      // GetActiveConnection returns connect string if connection  
      // is not open, else returns Connection object.  
      switch (vtConnect.vt) {  
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
  
   // Clean up objects before exit.  Release the IADORecordset Interface here     
   if (picRs)  
      picRs->Release();  
  
   if (pRstAuthors)  
      if (pRstAuthors->State == adStateOpen)  
         pRstAuthors->Close();  
  
}  
  
void PrintProviderError(_ConnectionPtr pConnection) {  
   // Print Provider Errors from Connection object.  
   // pErr is a record object in the Connection's Error collection.  
   ErrorPtr  pErr  = NULL;  
  
   if ( (pConnection->Errors->Count) > 0) {  
      long nCount = pConnection->Errors->Count;  
      // Collection ranges from 0 to nCount -1.  
      for (long i = 0 ; i < nCount ; i++) {  
         pErr = pConnection->Errors->GetItem(i);  
         printf("\t Error number: %x\t%s", pErr->Number, (LPCSTR) pErr->Description);  
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
1  
0  
```  
  
## Sample Output  
  
```  
Record 1 of 23  
Author: Abraham Bennet  
Location: Berkeley, CA  
  
Enter number of records to Move   
(positive or negative, Enter to quit): Record 2 of 23  
Author: Reginald Blotchet-Halls  
Location: Corvallis, OR  
  
Enter number of records to Move   
(positive or negative, Enter to quit):  
```  
  
## See Also  
 [Move Method (ADO)](../../../ado/reference/ado-api/move-method-ado.md)
