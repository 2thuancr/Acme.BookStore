import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CategoryDto extends AuditedEntityDto<string> {
  name?: string;
  description?: string;
}

export interface CategoryGetListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface CreateCategoryDto {
  name?: string;
  description?: string;
}

export interface UpdateCategoryDto {
  name?: string;
  description?: string;
}
