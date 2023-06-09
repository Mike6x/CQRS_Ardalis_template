﻿using Application.Common.Interfaces;

namespace Application.Common.Exporters;

public interface IExcelWriter 
{
    Stream WriteToStream<T>(IList<T> data);
}
