namespace CVRLua.Lua.LuaDefs
{
    delegate void StaticParseDelegate(LuaArgReader p_reader);
    delegate void InstanceParseDelegate(object p_obj, LuaArgReader p_reader);
}
