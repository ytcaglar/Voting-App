namespace Voting_App;
class User
{
    private bool _voteTrue = false;

    public string userName { get; set; }
    public string userPassword { get; set; }
    public int vote { get; set; }
    public bool voteTrue 
    { 
        get
        { return _voteTrue;

        } 
        set
        {
            _voteTrue=value;
        } 
    }
}
