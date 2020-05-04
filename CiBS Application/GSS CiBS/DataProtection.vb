Imports Microsoft.VisualBasic
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32

Public Class DataProtection
    Dim myKey As String
    'Dim cryptDES3 As New TripleDESCryptoServiceProvider()
    'Dim cryptMD5Hash As New MD5CryptoServiceProvider()

    'Function Decrypt(ByVal myString As String, ByVal myKey As String) As String
    '    cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
    '    cryptDES3.Mode = CipherMode.ECB
    '    Dim desdencrypt As ICryptoTransform = cryptDES3.CreateDecryptor()
    '    Dim buff() As Byte = Convert.FromBase64String(myString)
    '    Decrypt = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    'End Function

    'Function Encrypt(ByVal myString As String, ByVal myKey As String) As String
    '    cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey))
    '    cryptDES3.Mode = CipherMode.ECB
    '    Dim desdencrypt As ICryptoTransform = cryptDES3.CreateEncryptor()
    '    Dim MyASCIIEncoding = New ASCIIEncoding()
    '    Dim buff() As Byte = ASCIIEncoding.ASCII.GetBytes(myString)
    '    Encrypt = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length))
    'End Function
End Class
