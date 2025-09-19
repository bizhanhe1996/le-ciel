using LeCiel.Extras.Interfaces;
using LeCiel.Extras.Structs;
using LeCiel.Extras.Utils;

namespace LeCiel.Database.Repositories;

public class BaseRepository(AppContext context, Paginator paginator)
{
    protected readonly AppContext _context = context;
    protected readonly Paginator _paginator = paginator;

    public PaginationStruct PaginationStruct
    {
        get { return _paginator.PaginationStruct; }
    }
};
