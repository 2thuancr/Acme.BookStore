import type { AuditedEntityDto } from '@abp/ng.core';
import type { BookType } from './book-type.enum';
import type { BookStatus } from './book-status.enum';

export interface BookDto extends AuditedEntityDto<string> {
  name?: string;
  type?: BookType;
  publishDate?: string;
  price: number;
  status?: BookStatus;
}

export interface CreateUpdateBookDto {
  name: string;
  type: BookType;
  publishDate: string;
  price: number;
}
