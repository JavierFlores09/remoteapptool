// code sample taken from:
// https://code.msdn.microsoft.com/windowsapps/How-to-know-the-process-170ed5f3/sourcecode?fileId=151114&pathId=1558127374
// Modified to suit the needs of this application

namespace LockChecker.New
{
    public class LockChecker
    {
        public string CheckLock(string FileName)
        {
            return LockCheckType.LockCheck(FileName);
        }
    }
}