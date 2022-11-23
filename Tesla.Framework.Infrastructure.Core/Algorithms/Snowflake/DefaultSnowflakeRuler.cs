using Microsoft.Extensions.Options;

namespace Tesla.Framework.Infrastructure.Core.Algorithms.Snowflake
{
    public class DefaultSnowflakeRuler : ISnowflakeRuler
    {
        private SnowflakeOptions snowflakeRulerInfo;

        public DefaultSnowflakeRuler(IOptions<SnowflakeOptions> options)
        {
            snowflakeRulerInfo = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SnowflakeOptions GetRulerInfo()
        {
            return snowflakeRulerInfo;
        }
    }
}
