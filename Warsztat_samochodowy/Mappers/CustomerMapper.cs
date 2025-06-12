using Riok.Mapperly.Abstractions;
using Warsztat_samochodowy.Models;
using Warsztat_samochodowy.DTOs;

namespace Warsztat_samochodowy.Mappers;

[Mapper]
public partial class CustomerMapper
{
    public partial CustomerModel ToModel(CustomerCreateDto dto);

    public partial CustomerModel ToModel(CustomerEditDto dto);

    public partial CustomerListDto ToListDto(CustomerModel model);

    public partial CustomerEditDto ToEditDto(CustomerModel model);
}
