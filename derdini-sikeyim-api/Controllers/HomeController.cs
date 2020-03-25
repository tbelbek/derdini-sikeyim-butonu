using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace derdini_sikeyim_api.Controllers
{
    [ApiController]
    [Route("derdini")]
    public class HomeController : ControllerBase
    {
        private readonly ISqliteHelper _sqliteHelper;

        public HomeController(ISqliteHelper sqliteHelper)
        {
            _sqliteHelper = sqliteHelper;
        }

        [HttpGet]
        [Route("sikeyim")]
        public long Sikeyim(long entryId)
        {
            var data = _sqliteHelper.ReadEntryData(entryId);
            _sqliteHelper.ExecuteSql(data != null && data.FuckCount != 0
                ? $"UPDATE EntryLog Set FuckCount={data.FuckCount + 1} WHERE Id = {data.Id}"
                : $"INSERT INTO EntryLog (EntryId,FuckCount) VALUES ({entryId},1)");

            return _sqliteHelper.ReadEntryData(entryId).FuckCount;
        }
    }
}
