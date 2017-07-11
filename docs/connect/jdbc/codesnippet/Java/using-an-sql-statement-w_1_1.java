public static void executeStatement(Connection con) {
   try {
      String SQL = "SELECT LastName, FirstName FROM Person.Contact WHERE LastName = ?";
      PreparedStatement pstmt = con.prepareStatement(SQL);
      pstmt.setString(1, "Smith");
      ResultSet rs = pstmt.executeQuery();

      while (rs.next()) {
         System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));
      }
      rs.close();
      pstmt.close();
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}