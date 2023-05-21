namespace Voting_App;
class Category
{
    private int _voitCount = 0;
    public string CategoryName { get; set; }
    public int voitCount 
    { 
        get
        {
            return _voitCount;
        }
    }

    public void increaseMethod(){
        _voitCount++;
    }

    public void decreaseMethod(){
        _voitCount--;
    }
}
