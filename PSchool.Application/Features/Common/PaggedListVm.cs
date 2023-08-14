﻿namespace PSchool.Application.Features.Common;

public class PaggedListVm<T> where T : class
{
    public int Count { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public ICollection<T>? ListItems { get; set; }
}
