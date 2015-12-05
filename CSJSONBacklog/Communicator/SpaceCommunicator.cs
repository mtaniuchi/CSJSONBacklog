/* See the file "LICENSE" for the full license governing this code. */

using System.Collections.Generic;
using CSJSONBacklog.API;
using CSJSONBacklog.Model.Space;

namespace CSJSONBacklog.Communicator
{
    public class SpaceCommunicator : AbstractCommunicator, ISpaceAPI
    {
        public SpaceCommunicator(string spaceKey, string apiKey)
            : base(spaceKey, apiKey)
        {}

        /// <summary>
        /// Returns information about your space.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-space"/>
        public Space GetSpace()
        {
            return GetT<Space>(string.Format("https://{0}.backlog.jp/api/v2/space?apiKey={1}", SpaceKey, ApiKey));
        }

        /// <summary>
        /// Returns list of users in your space.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-users"/>
        public IEnumerable<User> GetUserList()
        {
            return GetT<IEnumerable<User>>(string.Format("https://{0}.backlog.jp/api/v2/users?apiKey={1}", SpaceKey, ApiKey));
        }

        /// <summary>
        /// Returns list of groups in your space.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-group"/>
        public IEnumerable<Group> GetGroupList()
        {
            return GetT<IEnumerable<Group>>(string.Format("https://{0}.backlog.jp/api/v2/groups?apiKey={1}", SpaceKey, ApiKey));
        }
    }
}

