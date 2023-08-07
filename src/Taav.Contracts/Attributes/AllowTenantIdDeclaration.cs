namespace Taav.Contracts.Attributes;

[AttributeUsage(
    AttributeTargets.Class,
    AllowMultiple = false,
    Inherited = true)]
public class AllowTenantIdDeclaration : Attribute 
{
    
}